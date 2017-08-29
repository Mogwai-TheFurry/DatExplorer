using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
        private DatDatabase _database = null;
        private CompressionType? _compressionFilter = null;

        private DateTime? _dateFilter = null;
        private DateFilterOptions? _dateFilterOptions = null;
        
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
                _database = new DatDatabase(openFileDialog.FileName);
                LoadTree();
            }
        }

        private void LoadTree()
        {
            if (_database == null)
                return;

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

            tsProgress.Text = $"Creating tree structure... (0 of {_database.AllFiles.Count})";
            tsProgress.Maximum = _database.AllFiles.Count;
            tsProgress.Minimum = 0;
            tsProgress.Value = 0;
            tsProgress.Width = this.Width - 50;

            this.Text = "Dat Explorer - " + Path.GetFileName(_database.FileName);

            Dictionary<byte, TreeNode> rootNodes = new Dictionary<byte, TreeNode>();

            for (int i = 0; i < _database.AllFiles.Count; i++)
            {
                var df = _database.AllFiles[i];

                if (_compressionFilter != null && df.CompressionLevel != _compressionFilter)
                    continue;

                if (_dateFilter != null && _dateFilterOptions != null &&
                    ((_dateFilterOptions == DateFilterOptions.On && _dateFilter.Value.Date != df.FileDate.Date)
                        || (_dateFilterOptions == DateFilterOptions.OnOrAfter && _dateFilter.Value.Date > df.FileDate.Date)
                        || (_dateFilterOptions == DateFilterOptions.OnOrBefore && _dateFilter.Value.Date < df.FileDate.Date)
                    ))
                    continue;

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
            }

            tsProgress.Visible = false;
            tvDatViewer.Enabled = true;
            this.Cursor = lastCursor;
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
            bool isFiltered = byDateToolStripMenuItem.Checked;

            if (!isFiltered)
            {
                fDatePicker datePicker = new fDatePicker();
                DialogResult dr = datePicker.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    _dateFilter = datePicker.SelectedDate.Value;
                    _dateFilterOptions = datePicker.SelectedOption.Value;
                    byDateToolStripMenuItem.Checked = true;
                    LoadTree();
                }
            }
            else
            {
                _dateFilter = null;
                _dateFilterOptions = null;
                byDateToolStripMenuItem.Checked = false;
                LoadTree();
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
            pbPreview.Image = null;
            pbPreview.Tag = null;

            // Select the clicked node
            if (df != null)
            {
                StringBuilder sbInfo = new StringBuilder();
                StringBuilder sbRaw = new StringBuilder();
                StringBuilder sbAscii = new StringBuilder();

                string extraInfo = null;
                string contentDescription = null;

                byte[] actualContent = null;
                byte[] header1 = null;
                byte[] header2 = null;

                if (df.CompressionLevel == CompressionType.Default || df.CompressionLevel == CompressionType.Maximum)
                {
                    // for compressed files, the header is only an unknown dword
                    header1 = _database.GetData(df.FileOffset + 8, 4);
                    var compressed = _database.GetData(df.FileOffset + 12, (int)df.Size1);

                    try
                    {
                        actualContent = Ionic.Zlib.ZlibStream.UncompressBuffer(compressed);

                        // inside the compressed content is the file id (dword), and an unknown dword
                        // of a very low (<256, typically) value.
                        header2 = actualContent.Take(8).ToArray();
                        actualContent = actualContent.Skip(8).ToArray();
                        var compressionRatio = 100 * (decimal)compressed.Length / (decimal)actualContent.Length;
                        contentDescription = $"Content was compressed by {compressionRatio:#} percent.  Viewing decompressed content.";
                    }
                    catch (Exception ex)
                    {
                        byte[] fakeHeader = new byte[2];
                        contentDescription = "Unable to decompress content.  Showing raw content instead.";
                        header1 = new byte[0];
                        actualContent = compressed;
                    }
                }
                else
                {
                    // for uncompressed files, the header is the file id (dword) and the file size (dword)
                    header1 = _database.GetData(df.FileOffset + 8, 8);
                    actualContent = _database.GetData(df.FileOffset + 16, (int)BitConverter.ToUInt32(header1, 4));
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

                // raw data tab
                if (!string.IsNullOrWhiteSpace(contentDescription))
                    sbRaw.AppendLine(contentDescription);

                sbRaw.AppendLine().AppendLine($"File Header ({header1.Length} bytes)");
                sbRaw.AppendLine(header1.PrintBytes());

                if (header2 != null)
                {
                    sbRaw.AppendLine().AppendLine($"Compressed Header ({header2.Length} bytes)");
                    sbRaw.AppendLine(header2.PrintBytes());
                }

                sbRaw.AppendLine().AppendLine($"Content ({actualContent.Length} bytes)");
                sbRaw.AppendLine(actualContent.PrintBytes());

                // ascii tab
                sbAscii.AppendLine($"File Header ({header1.Length} bytes)");
                sbAscii.AppendLine(header1.PrintAsciiBytes());

                if (header2 != null)
                {
                    sbAscii.AppendLine().AppendLine($"Compressed Header ({header2.Length} bytes)");
                    sbAscii.AppendLine(header2.PrintAsciiBytes());
                }

                sbAscii.AppendLine().AppendLine($"Content ({actualContent.Length} bytes)");
                sbAscii.Append(actualContent.PrintAsciiBytes());

                switch (fileType)
                {
                    case KnownFileType.DXT1:
                    case KnownFileType.DXT3:
                    case KnownFileType.DXT5:
                        {
                            uint height = BitConverter.ToUInt32(actualContent, 0);
                            uint width = BitConverter.ToUInt32(actualContent, 4);
                            try
                            {
                                var dds = DdsFile.ConvertFromDxt(fileType, actualContent.Skip(16).ToArray(), width, height);
                                pbPreview.Image = DdsFile.DdsToBmp((int)width, (int)height, dds);
                                pbPreview.Tag = df;

                                sbInfo.AppendLine("Image Content available for preview (use Mouse Wheel to zoom).");
                            }
                            catch (Exception ex)
                            {
                                sbInfo.AppendLine("Image Content Detected but no preview is available.");
                            }
                            break;
                        }
                        break;
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
                    // for compressed files, the header is only an unknown dword
                    var compressed = _database.GetData(df.FileOffset + 12, (int)df.Size1);

                    try
                    {
                        actualContent = Ionic.Zlib.ZlibStream.UncompressBuffer(compressed);

                        // inside the compressed content is the file id (dword), and an unknown dword
                        // of a very low (<256, typically) value.
                        actualContent = actualContent.Skip(8).ToArray();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to decompress content.  Exporting compressed content instead.");
                        actualContent = compressed;
                    }
                }
                else
                {
                    // for uncompressed files, the header is the file id (dword) and the file size (dword)
                    header = _database.GetData(df.FileOffset + 8, 8);
                    actualContent = _database.GetData(df.FileOffset + 16, (int)BitConverter.ToUInt32(header, 4));
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

        private void Form1_Load(object sender, EventArgs e)
        {
            //pbPreview.MouseWheel += PbPreview_MouseWheel;
            //pbPreview.Paint += PbPreview_Paint;
        }
        
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UpdateCompressionFilter()
        {
            allCompressionTypes.Checked = (_compressionFilter == null);
            noCompression.Checked = (_compressionFilter == CompressionType.Uncompressed);
            maximumCompression.Checked = (_compressionFilter == CompressionType.Maximum);
            unknownCompression.Checked = (_compressionFilter == CompressionType.Unknown_Value_2);
            defaultCompression.Checked = (_compressionFilter == CompressionType.Default);
        }

        private void uncompressedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _compressionFilter = CompressionType.Uncompressed;
            UpdateCompressionFilter();
            LoadTree();
        }

        private void showAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _compressionFilter = null;
            UpdateCompressionFilter();
            LoadTree();
        }

        private void maximumCompression_Click(object sender, EventArgs e)
        {
            _compressionFilter = CompressionType.Maximum;
            UpdateCompressionFilter();
            LoadTree();
        }

        private void unknownCompression_Click(object sender, EventArgs e)
        {
            _compressionFilter = CompressionType.Unknown_Value_2;
            UpdateCompressionFilter();
            LoadTree();
        }

        private void defaultCompression_Click(object sender, EventArgs e)
        {
            _compressionFilter = CompressionType.Default;
            UpdateCompressionFilter();
            LoadTree();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pbPreview.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                DatFile df = (DatFile)pbPreview.Tag;
                sfd.FileName = df.UserDefinedName ?? df.FileId.ToString("X8") + ".bmp";
                var save = sfd.ShowDialog(this);

                if (save == DialogResult.OK)
                    pbPreview.Image.Save(sfd.FileName);
            }
        }
    }
}
