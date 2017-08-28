using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mogwai.DDO.Explorer.UI
{
    public partial class Form1 : Form
    {
        private string _currentFile = null;
        private DatDatabase _database = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            Application.DoEvents();

            if (result == DialogResult.OK)
            {
                // could check for opening of the same file, but while we're debugging this
                // that's probably not the most helpful idea
                var lastCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                tvDatViewer.Enabled = false;
                tvDatViewer.Nodes.Clear();

                tsProgress.Visible = true;
                tsProgress.Text = "Loading " + openFileDialog.FileName + " and scanning structures...";

                // forces the UI thread to finish its work and update the status strip text
                Application.DoEvents();

                _database = new DatDatabase(openFileDialog.FileName);
                tsProgress.Text = $"Creating tree structure... (0 of {_database.AllFiles.Count})";
                tsProgress.Maximum = _database.AllFiles.Count;
                tsProgress.Minimum = 0;
                tsProgress.Value = 0;
                tsProgress.Width = this.Width - 50;

                this.Text = "Dat Explorer - " + Path.GetFileName(_database.FileName);

                Dictionary<byte, TreeNode> rootNodes = new Dictionary<byte, TreeNode>();

                _database.AllFiles.ForEach(df =>
                {
                    byte idSegment = (byte)(df.FileId >> 24);
                    TreeNode parentNode = null;

                    if (!rootNodes.ContainsKey(idSegment))
                    {
                        parentNode = new TreeNode("0x" + idSegment.ToString("X2"));
                        tvDatViewer.Nodes.Add(parentNode);
                        rootNodes.Add(idSegment, parentNode);
                    }
                    else
                    {
                        parentNode = rootNodes[idSegment];
                    }

                    TreeNode thisNode = new TreeNode(df.FileId.ToString("X8"));
                    if (!string.IsNullOrWhiteSpace(df.UserDefinedName))
                    {
                        thisNode.Text += " - " + df.UserDefinedName;
                    }
                    thisNode.Tag = df; // copy the dat file into the node

                    // build child nodes of the properties
                    thisNode.Nodes.Add("File Address: " + df.FileOffset);
                    thisNode.Nodes.Add("Internal Type: 0x" + df.FileType.ToString("X8"));
                    thisNode.Nodes.Add("File Modified: " + df.FileDate);
                    thisNode.Nodes.Add("File Size 1: " + df.Size1);
                    thisNode.Nodes.Add("File Size 2: " + df.Size2);
                    thisNode.Nodes.Add("File Version: " + df.Version);
                    thisNode.Nodes.Add("Unknown 1: 0x" + df.Unknown1.ToString("X8"));
                    thisNode.Nodes.Add("Unknown 2: 0x" + df.Unknown2.ToString("X8"));
                    thisNode.ContextMenuStrip = cmsNode;


                    // TODO: implement sorting
                    parentNode.Nodes.Add(thisNode);
                    tsProgress.Value++;

                    tsProgress.Text = $"Creating tree structure... ({tsProgress.Value} of {_database.AllFiles.Count})";
                    // Application.DoEvents();
                });

                tsProgress.Visible = false;
                tvDatViewer.Enabled = true;
                this.Cursor = lastCursor;
            }
        }

        private void Clear()
        {
            tvDatViewer.Nodes.Clear();
            _database = null;
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void byDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fDatePicker datePicker = new fDatePicker();
            DialogResult dr = datePicker.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                DateTime val = datePicker.SelectedDate.Value;
                var option = datePicker.SelectedOption.Value;


            }
        }

        private void byFileIdToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void byTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void byInternalTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            var source = (menuItem.Owner as ContextMenuStrip).SourceControl as TreeView;

            DatFile df = source?.SelectedNode?.Tag as DatFile;
            tbInfo.Text = null;
            tbRaw.Text = null;
            tbAscii.Text = null;

            // Select the clicked node
            if (df != null)
            {
                StringBuilder sbInfo = new StringBuilder();
                StringBuilder sbRaw = new StringBuilder();
                StringBuilder sbAscii = new StringBuilder();
                StringBuilder sbDecomp = new StringBuilder();
                string contentDescription = null;
                
                byte[] actualContent = null;
                byte[] header = null;

                if (df.CompressionLevel == CompressionType.Default || df.CompressionLevel == CompressionType.Maximum)
                {
                    header = _database.GetData(df.FileOffset + 8, 4);
                    var compressed = _database.GetData(df.FileOffset + 12, (int)df.Size1);
                    actualContent = Ionic.Zlib.ZlibStream.UncompressBuffer(compressed);

                    // header = actualContent.Take(8).ToArray();
                    var compressionRatio = 100 * (decimal)compressed.Length / (decimal)actualContent.Length;
                    contentDescription = $"Content was compressed by {compressionRatio:#} percent.  Viewing decompressed content.";
                }
                else
                {
                    header = _database.GetData(df.FileOffset + 8, 8);
                    actualContent = _database.GetData(df.FileOffset + 16, (int)df.Size1);
                }

                sbInfo.AppendLine("File ID: " + df.FileId.ToString("X8"));
                sbInfo.AppendLine("Offset: 0x" + df.FileOffset.ToString("X"));
                sbInfo.AppendLine("File Date: " + df.FileDate);
                sbInfo.AppendLine("Version: " + df.Version);
                sbInfo.AppendLine("Compression: " + df.CompressionLevel.ToString());
                sbInfo.AppendLine("File Size: " + df.Size1);
                sbInfo.AppendLine("Dat Space Consumed: " + df.Size2);
                sbInfo.AppendLine("Type: 0x" + df.FileType.ToString("X8"));
                sbInfo.AppendLine("Unknown 1: 0x" + df.Unknown1.ToString("X8"));
                sbInfo.AppendLine("Unknown 2: 0x" + df.Unknown2.ToString("X8"));

                var fileType = DatFile.GetActualFileType(actualContent);

                switch (fileType)
                {
                    case KnownFileType.DXT1:
                    case KnownFileType.DXT3:
                    case KnownFileType.DXT5:
                        sbInfo.AppendLine("Image Content Detected.");
                        break;
                }

                if (!string.IsNullOrWhiteSpace(contentDescription))
                    sbRaw.AppendLine(contentDescription).AppendLine();
                
                sbRaw.AppendLine($"Header ({header.Length} bytes)");
                for (int i = 0; i < header.Length; i++)
                {
                    sbRaw.Append(header[i].ToString("X2"));

                    if ((i + 1) % 4 == 0)
                        sbRaw.Append(" ");

                    if ((i + 1) % 16 == 0)
                        sbRaw.AppendLine();
                }

                sbRaw.AppendLine().AppendLine();
                sbRaw.AppendLine($"Content ({actualContent.Length} bytes)");
                for (int i = 0; i < actualContent.Length; i++)
                {
                    sbRaw.Append(actualContent[i].ToString("X2"));

                    if ((i + 1) % 4 == 0)
                        sbRaw.Append(" ");

                    if ((i + 1) % 16 == 0)
                        sbRaw.AppendLine();
                }

                sbAscii.AppendLine($"Header ({header.Length} bytes)");
                for (int i = 0; i < header.Length; i++)
                {
                    byte thisByte = header[i];
                    char thisChar = '.';

                    if (thisByte > 31)
                        thisChar = Encoding.ASCII.GetString(header, i, 1)[0];

                    sbAscii.Append(thisChar);

                    if ((i + 1) % 4 == 0)
                        sbAscii.Append(" ");

                    if ((i + 1) % 16 == 0)
                        sbAscii.AppendLine();
                }

                sbAscii.AppendLine().AppendLine();
                sbAscii.AppendLine($"Content ({actualContent.Length} bytes)");
                for (int i = 0; i < actualContent.Length; i++)
                {
                    byte thisByte = actualContent[i];
                    char thisChar = '.';

                    if (thisByte > 31)
                        thisChar = Encoding.ASCII.GetString(actualContent, i, 1)[0];

                    sbAscii.Append(thisChar);

                    if ((i + 1) % 4 == 0)
                        sbAscii.Append(" ");

                    if ((i + 1) % 16 == 0)
                        sbAscii.AppendLine();
                }
                
                tbInfo.Text = sbInfo.ToString();
                tbRaw.Text = sbRaw.ToString();
                tbAscii.Text = sbAscii.ToString();
            }
        }

        private void tvDatViewer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ((TreeView)sender).SelectedNode = e.Node;
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            var source = (menuItem.Owner as ContextMenuStrip).SourceControl as TreeView;

            DatFile df = source?.SelectedNode?.Tag as DatFile;

            // Select the clicked node
            if (df != null)
            {
                byte[] actualContent = null;
                byte[] header = null;

                if (df.CompressionLevel == CompressionType.Default || df.CompressionLevel == CompressionType.Maximum)
                {
                    header = _database.GetData(df.FileOffset + 8, 4);
                    var compressed = _database.GetData(df.FileOffset + 12, (int)df.Size1);
                    actualContent = Ionic.Zlib.ZlibStream.UncompressBuffer(compressed);
                }
                else
                {
                    header = _database.GetData(df.FileOffset + 8, 8);
                    actualContent = _database.GetData(df.FileOffset + 16, (int)df.Size1);
                }

                string extension = "bin";
                var knownType = DatFile.GetActualFileType(actualContent);

                if (knownType != KnownFileType.Unknown)
                    extension = EnumHelpers.GetFileExtension(knownType);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = df.UserDefinedName ?? df.FileId.ToString("X8") + "." + extension;
                var save = sfd.ShowDialog(this);

                if (save == DialogResult.OK)
                    File.WriteAllBytes(sfd.FileName, actualContent);
            }

        }

        private void cmsNode_Opening(object sender, CancelEventArgs e)
        {
            playToolStripMenuItem.Visible = false;

            return;

            // to be implemented later

            var source = (sender as ContextMenuStrip).SourceControl as TreeView;

            DatFile df = source?.SelectedNode?.Tag as DatFile;

            // Select the clicked node
            if (df != null)
            {
                var buffer = _database.GetData(df.FileOffset + 16, 16);
                var knownType = DatFile.GetActualFileType(buffer);

                switch (knownType)
                {
                    case KnownFileType.Ogg:
                    case KnownFileType.Wave:
                        playToolStripMenuItem.Visible = true;
                        break;
                }

            }

        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            var source = (menuItem.Owner as ContextMenuStrip).SourceControl as TreeView;

            DatFile df = source?.SelectedNode?.Tag as DatFile;

            // Select the clicked node
            if (df != null)
            {
                var buffer = _database.GetData(df.FileOffset + 16, 16);
                var knownType = DatFile.GetActualFileType(buffer);

                var fileHeader = _database.GetData(df.FileOffset + 8, 8);
                uint actualSize = BitConverter.ToUInt32(fileHeader, 4);
                buffer = _database.GetData(df.FileOffset + 16, (int)actualSize);

                switch (knownType)
                {
                    case KnownFileType.Ogg:
                        
                        break;
                    case KnownFileType.Wave:
                        break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
