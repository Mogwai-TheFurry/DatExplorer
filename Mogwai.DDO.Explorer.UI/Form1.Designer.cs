namespace Mogwai.DDO.Explorer.UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tvDatViewer = new System.Windows.Forms.TreeView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRecent1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byFileIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byInternalTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoDecompressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoDetectContentTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tbPreview = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tbPreviewDecoded = new System.Windows.Forms.TextBox();
            this.lblPreview = new System.Windows.Forms.Label();
            this.cmsNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.cmsNode.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvDatViewer
            // 
            this.tvDatViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDatViewer.HideSelection = false;
            this.tvDatViewer.Location = new System.Drawing.Point(0, 0);
            this.tvDatViewer.Name = "tvDatViewer";
            this.tvDatViewer.Size = new System.Drawing.Size(320, 454);
            this.tvDatViewer.TabIndex = 0;
            this.tvDatViewer.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvDatViewer_NodeMouseClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsProgress});
            this.statusStrip.Location = new System.Drawing.Point(0, 484);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(933, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsProgress
            // 
            this.tsProgress.Name = "tsProgress";
            this.tsProgress.Size = new System.Drawing.Size(400, 16);
            this.tsProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.tsProgress.ToolTipText = "tool tip text";
            this.tsProgress.Visible = false;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.searchToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(933, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.tsmiRecent,
            this.toolStripSeparator1,
            this.tsmiExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(37, 20);
            this.tsmiFile.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // tsmiRecent
            // 
            this.tsmiRecent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRecent1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.tsmiRecent.Name = "tsmiRecent";
            this.tsmiRecent.Size = new System.Drawing.Size(146, 22);
            this.tsmiRecent.Text = "&Recent";
            // 
            // tsmiRecent1
            // 
            this.tsmiRecent1.Name = "tsmiRecent1";
            this.tsmiRecent1.ShortcutKeyDisplayString = "";
            this.tsmiRecent1.Size = new System.Drawing.Size(80, 22);
            this.tsmiRecent1.Text = "&1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem2.Text = "&2";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem3.Text = "&3";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem4.Text = "&4";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(146, 22);
            this.tsmiExit.Text = "E&xit";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byDateToolStripMenuItem,
            this.byFileIdToolStripMenuItem,
            this.byTypeToolStripMenuItem,
            this.byInternalTypeToolStripMenuItem});
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.searchToolStripMenuItem.Text = "Search";
            // 
            // byDateToolStripMenuItem
            // 
            this.byDateToolStripMenuItem.Name = "byDateToolStripMenuItem";
            this.byDateToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.byDateToolStripMenuItem.Text = "By Date";
            this.byDateToolStripMenuItem.Click += new System.EventHandler(this.byDateToolStripMenuItem_Click);
            // 
            // byFileIdToolStripMenuItem
            // 
            this.byFileIdToolStripMenuItem.Name = "byFileIdToolStripMenuItem";
            this.byFileIdToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.byFileIdToolStripMenuItem.Text = "By File Id";
            this.byFileIdToolStripMenuItem.Click += new System.EventHandler(this.byFileIdToolStripMenuItem_Click);
            // 
            // byTypeToolStripMenuItem
            // 
            this.byTypeToolStripMenuItem.Name = "byTypeToolStripMenuItem";
            this.byTypeToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.byTypeToolStripMenuItem.Text = "By Content Type";
            this.byTypeToolStripMenuItem.Click += new System.EventHandler(this.byTypeToolStripMenuItem_Click);
            // 
            // byInternalTypeToolStripMenuItem
            // 
            this.byInternalTypeToolStripMenuItem.Name = "byInternalTypeToolStripMenuItem";
            this.byInternalTypeToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.byInternalTypeToolStripMenuItem.Text = "By Internal Type";
            this.byInternalTypeToolStripMenuItem.Click += new System.EventHandler(this.byInternalTypeToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoDecompressToolStripMenuItem,
            this.autoDetectContentTypeToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // autoDecompressToolStripMenuItem
            // 
            this.autoDecompressToolStripMenuItem.Checked = true;
            this.autoDecompressToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoDecompressToolStripMenuItem.Name = "autoDecompressToolStripMenuItem";
            this.autoDecompressToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.autoDecompressToolStripMenuItem.Text = "Auto Decompress";
            // 
            // autoDetectContentTypeToolStripMenuItem
            // 
            this.autoDetectContentTypeToolStripMenuItem.Checked = true;
            this.autoDetectContentTypeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoDetectContentTypeToolStripMenuItem.Name = "autoDetectContentTypeToolStripMenuItem";
            this.autoDetectContentTypeToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.autoDetectContentTypeToolStripMenuItem.Text = "Auto Detect Content Type";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "dat";
            this.openFileDialog.Filter = "Dat Files|*.dat";
            this.openFileDialog.Title = "Select a dat file";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // tbPreview
            // 
            this.tbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPreview.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPreview.Location = new System.Drawing.Point(0, 0);
            this.tbPreview.Multiline = true;
            this.tbPreview.Name = "tbPreview";
            this.tbPreview.ReadOnly = true;
            this.tbPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPreview.Size = new System.Drawing.Size(301, 454);
            this.tbPreview.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvDatViewer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.lblPreview);
            this.splitContainer1.Size = new System.Drawing.Size(933, 454);
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tbPreview);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tbPreviewDecoded);
            this.splitContainer2.Size = new System.Drawing.Size(609, 454);
            this.splitContainer2.SplitterDistance = 301;
            this.splitContainer2.TabIndex = 5;
            // 
            // tbPreviewDecoded
            // 
            this.tbPreviewDecoded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPreviewDecoded.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPreviewDecoded.Location = new System.Drawing.Point(0, 0);
            this.tbPreviewDecoded.Multiline = true;
            this.tbPreviewDecoded.Name = "tbPreviewDecoded";
            this.tbPreviewDecoded.ReadOnly = true;
            this.tbPreviewDecoded.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPreviewDecoded.Size = new System.Drawing.Size(304, 454);
            this.tbPreviewDecoded.TabIndex = 4;
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Location = new System.Drawing.Point(3, 10);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(0, 13);
            this.lblPreview.TabIndex = 4;
            // 
            // cmsNode
            // 
            this.cmsNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.previewToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.playToolStripMenuItem});
            this.cmsNode.Name = "cmsNode";
            this.cmsNode.Size = new System.Drawing.Size(153, 114);
            this.cmsNode.Opening += new System.ComponentModel.CancelEventHandler(this.cmsNode_Opening);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.renameToolStripMenuItem.Text = "&Rename";
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.previewToolStripMenuItem.Text = "&Preview";
            this.previewToolStripMenuItem.Click += new System.EventHandler(this.previewToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.exportToolStripMenuItem.Text = "&Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.playToolStripMenuItem.Text = "&Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 506);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "Form1";
            this.Text = "Dat Explorer";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.cmsNode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvDatViewer;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiRecent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byFileIdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byInternalTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoDecompressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoDetectContentTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiRecent1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripProgressBar tsProgress;
        private System.Windows.Forms.TextBox tbPreview;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip cmsNode;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox tbPreviewDecoded;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
    }
}

