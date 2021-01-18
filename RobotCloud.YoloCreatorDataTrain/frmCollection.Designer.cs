namespace RobotCloud.YoloCreatorDataTrain
{
    partial class frmCollection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCollection));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnNewImg = new System.Windows.Forms.ToolStripButton();
            this.tsBtnLoadImage = new System.Windows.Forms.ToolStripButton();
            this.tsTxtSelectedImg = new System.Windows.Forms.ToolStripTextBox();
            this.tsBtnRunCmd = new System.Windows.Forms.ToolStripButton();
            this.tsTxtCmdYolo = new System.Windows.Forms.ToolStripTextBox();
            this.tsTxtCmdYoloTiny = new System.Windows.Forms.ToolStripTextBox();
            this.tsBtnNewCollectionFromFolder = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsStatusBottom = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lsvFiles = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsBtnCreateYoloDataset = new System.Windows.Forms.ToolStripButton();
            this.tsBtnRebuildPlainData = new System.Windows.Forms.ToolStripButton();
            this.tsBtnAddExistedFolder = new System.Windows.Forms.ToolStripButton();
            this.lsvCollection = new System.Windows.Forms.ListView();
            this.tsBtnCropRebuild = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1340, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnNewImg,
            this.tsBtnLoadImage,
            this.tsTxtSelectedImg,
            this.tsBtnRunCmd,
            this.tsTxtCmdYolo,
            this.tsTxtCmdYoloTiny,
            this.tsBtnNewCollectionFromFolder});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1340, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnNewImg
            // 
            this.tsBtnNewImg.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnNewImg.Image")));
            this.tsBtnNewImg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnNewImg.Name = "tsBtnNewImg";
            this.tsBtnNewImg.Size = new System.Drawing.Size(75, 22);
            this.tsBtnNewImg.Text = "New img";
            this.tsBtnNewImg.Click += new System.EventHandler(this.tsBtnNewImg_Click);
            // 
            // tsBtnLoadImage
            // 
            this.tsBtnLoadImage.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnLoadImage.Image")));
            this.tsBtnLoadImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnLoadImage.Name = "tsBtnLoadImage";
            this.tsBtnLoadImage.Size = new System.Drawing.Size(77, 22);
            this.tsBtnLoadImage.Text = "Load img";
            this.tsBtnLoadImage.Click += new System.EventHandler(this.tsBtnLoadImage_Click);
            // 
            // tsTxtSelectedImg
            // 
            this.tsTxtSelectedImg.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tsTxtSelectedImg.Name = "tsTxtSelectedImg";
            this.tsTxtSelectedImg.Size = new System.Drawing.Size(500, 25);
            // 
            // tsBtnRunCmd
            // 
            this.tsBtnRunCmd.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnRunCmd.Image")));
            this.tsBtnRunCmd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRunCmd.Name = "tsBtnRunCmd";
            this.tsBtnRunCmd.Size = new System.Drawing.Size(75, 22);
            this.tsBtnRunCmd.Text = "Run cmd";
            this.tsBtnRunCmd.Click += new System.EventHandler(this.tsBtnRunCmd_Click);
            // 
            // tsTxtCmdYolo
            // 
            this.tsTxtCmdYolo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tsTxtCmdYolo.Name = "tsTxtCmdYolo";
            this.tsTxtCmdYolo.Size = new System.Drawing.Size(100, 25);
            // 
            // tsTxtCmdYoloTiny
            // 
            this.tsTxtCmdYoloTiny.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tsTxtCmdYoloTiny.Name = "tsTxtCmdYoloTiny";
            this.tsTxtCmdYoloTiny.Size = new System.Drawing.Size(100, 25);
            // 
            // tsBtnNewCollectionFromFolder
            // 
            this.tsBtnNewCollectionFromFolder.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnNewCollectionFromFolder.Image")));
            this.tsBtnNewCollectionFromFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnNewCollectionFromFolder.Name = "tsBtnNewCollectionFromFolder";
            this.tsBtnNewCollectionFromFolder.Size = new System.Drawing.Size(175, 22);
            this.tsBtnNewCollectionFromFolder.Text = "New Collection From Folder";
            this.tsBtnNewCollectionFromFolder.Click += new System.EventHandler(this.tsBtnNewCollectionFromFolder_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatusBottom});
            this.statusStrip1.Location = new System.Drawing.Point(0, 600);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1340, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsStatusBottom
            // 
            this.tsStatusBottom.Name = "tsStatusBottom";
            this.tsStatusBottom.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1340, 551);
            this.splitContainer1.SplitterDistance = 827;
            this.splitContainer1.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lsvFiles);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(827, 551);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File(s)";
            // 
            // lsvFiles
            // 
            this.lsvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvFiles.FullRowSelect = true;
            this.lsvFiles.GridLines = true;
            this.lsvFiles.HideSelection = false;
            this.lsvFiles.Location = new System.Drawing.Point(3, 16);
            this.lsvFiles.MultiSelect = false;
            this.lsvFiles.Name = "lsvFiles";
            this.lsvFiles.Size = new System.Drawing.Size(821, 532);
            this.lsvFiles.TabIndex = 0;
            this.lsvFiles.UseCompatibleStateImageBehavior = false;
            this.lsvFiles.View = System.Windows.Forms.View.Details;
            this.lsvFiles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lsvFiles_ItemSelectionChanged);
            this.lsvFiles.SelectedIndexChanged += new System.EventHandler(this.lsvFiles_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toolStrip2);
            this.groupBox1.Controls.Add(this.lsvCollection);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 551);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Collection (Folder)";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnCreateYoloDataset,
            this.tsBtnRebuildPlainData,
            this.tsBtnAddExistedFolder,
            this.tsBtnCropRebuild});
            this.toolStrip2.Location = new System.Drawing.Point(3, 16);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(503, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsBtnCreateYoloDataset
            // 
            this.tsBtnCreateYoloDataset.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnCreateYoloDataset.Image")));
            this.tsBtnCreateYoloDataset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnCreateYoloDataset.Name = "tsBtnCreateYoloDataset";
            this.tsBtnCreateYoloDataset.Size = new System.Drawing.Size(116, 22);
            this.tsBtnCreateYoloDataset.Text = "Create Cmd Yolo";
            this.tsBtnCreateYoloDataset.Click += new System.EventHandler(this.tsBtnCreateYoloDataset_Click);
            // 
            // tsBtnRebuildPlainData
            // 
            this.tsBtnRebuildPlainData.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnRebuildPlainData.Image")));
            this.tsBtnRebuildPlainData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRebuildPlainData.Name = "tsBtnRebuildPlainData";
            this.tsBtnRebuildPlainData.Size = new System.Drawing.Size(93, 22);
            this.tsBtnRebuildPlainData.Text = "Rebuild data";
            this.tsBtnRebuildPlainData.Click += new System.EventHandler(this.tsBtnRebuildPlainData_Click);
            // 
            // tsBtnAddExistedFolder
            // 
            this.tsBtnAddExistedFolder.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnAddExistedFolder.Image")));
            this.tsBtnAddExistedFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAddExistedFolder.Name = "tsBtnAddExistedFolder";
            this.tsBtnAddExistedFolder.Size = new System.Drawing.Size(123, 22);
            this.tsBtnAddExistedFolder.Text = "Add existed folder";
            this.tsBtnAddExistedFolder.Click += new System.EventHandler(this.tsBtnAddExistedFolder_Click);
            // 
            // lsvCollection
            // 
            this.lsvCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvCollection.FullRowSelect = true;
            this.lsvCollection.GridLines = true;
            this.lsvCollection.HideSelection = false;
            this.lsvCollection.Location = new System.Drawing.Point(3, 16);
            this.lsvCollection.MultiSelect = false;
            this.lsvCollection.Name = "lsvCollection";
            this.lsvCollection.Size = new System.Drawing.Size(503, 532);
            this.lsvCollection.TabIndex = 1;
            this.lsvCollection.UseCompatibleStateImageBehavior = false;
            this.lsvCollection.View = System.Windows.Forms.View.Details;
            this.lsvCollection.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lsvCollection_ItemSelectionChanged);
            this.lsvCollection.SelectedIndexChanged += new System.EventHandler(this.lsvCollection_SelectedIndexChanged);
            this.lsvCollection.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsvCollection_MouseClick);
            // 
            // tsBtnCropRebuild
            // 
            this.tsBtnCropRebuild.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnCropRebuild.Image")));
            this.tsBtnCropRebuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnCropRebuild.Name = "tsBtnCropRebuild";
            this.tsBtnCropRebuild.Size = new System.Drawing.Size(125, 22);
            this.tsBtnCropRebuild.Text = "Auto Crop Rebuild";
            this.tsBtnCropRebuild.Click += new System.EventHandler(this.tsBtnCropRebuild_Click);
            // 
            // frmCollection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 622);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmCollection";
            this.Load += new System.EventHandler(this.formCollection_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lsvFiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lsvCollection;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusBottom;
        private System.Windows.Forms.ToolStripButton tsBtnLoadImage;
        private System.Windows.Forms.ToolStripTextBox tsTxtSelectedImg;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsBtnCreateYoloDataset;
        private System.Windows.Forms.ToolStripButton tsBtnRunCmd;
        private System.Windows.Forms.ToolStripButton tsBtnNewImg;
        private System.Windows.Forms.ToolStripButton tsBtnRebuildPlainData;
        private System.Windows.Forms.ToolStripButton tsBtnAddExistedFolder;
        private System.Windows.Forms.ToolStripTextBox tsTxtCmdYolo;
        private System.Windows.Forms.ToolStripTextBox tsTxtCmdYoloTiny;
        private System.Windows.Forms.ToolStripButton tsBtnNewCollectionFromFolder;
        private System.Windows.Forms.ToolStripButton tsBtnCropRebuild;
    }
}