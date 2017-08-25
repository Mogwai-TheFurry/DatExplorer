using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

            // Select the clicked node
            if (df != null)
            {
                uint maxPreviewSize = 2048;
                uint len = Math.Min(maxPreviewSize, Math.Max(df.Size1, df.Size2));
                var buffer = _database.GetData(df.FileOffset + 8, (int)len);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("File ID: " + df.FileId.ToString("X8"));
                sb.AppendLine("Offset: 0x" + df.FileOffset.ToString("X"));
                sb.AppendLine("File Date: " + df.FileDate);
                sb.AppendLine("Version: " + df.Version);
                sb.AppendLine("Size 1: " + df.Size1);
                sb.AppendLine("Size 2: " + df.Size2);
                sb.AppendLine("Type: 0x" + df.FileType.ToString("X8"));
                sb.AppendLine("Unknown 1: 0x" + df.Unknown1.ToString("X8"));
                sb.AppendLine("Unknown 2: 0x" + df.Unknown2.ToString("X8"));
                sb.AppendLine();

                StringBuilder sbAlt = new StringBuilder();
                sbAlt.AppendLine().AppendLine().AppendLine().AppendLine().AppendLine().AppendLine().AppendLine().AppendLine().AppendLine();
                sbAlt.AppendLine();

                for (int i = 0; i < len; i++)
                {
                    sb.Append(buffer[i].ToString("X2"));
                    char thisChar = '.';

                    if (buffer[i] > 31)
                        thisChar = Encoding.ASCII.GetString(buffer, i, 1)[0];

                    sbAlt.Append(thisChar);

                    if ((i + 1) % 4 == 0)
                    {
                        sb.Append(" ");
                        sbAlt.Append(" ");
                    }

                    if ((i + 1) % 16 == 0)
                    {
                        sb.AppendLine();
                        sbAlt.AppendLine();
                    }
                }

                tbPreview.Text = sb.ToString();
                tbPreviewDecoded.Text = sbAlt.ToString();
            }
        }

        private void tvDatViewer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ((TreeView)sender).SelectedNode = e.Node;
        }
    }
}
