namespace RobotCloud.YoloCreatorDataTrain
{
    partial class frmImageLabling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImageLabling));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.tsLblCollectionName = new System.Windows.Forms.ToolStripLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splcMain = new System.Windows.Forms.SplitContainer();
            this.grbMainImg = new System.Windows.Forms.GroupBox();
            this.pnlPicture = new System.Windows.Forms.Panel();
            this.picMain = new RobotCloud.YoloCreatorDataTrain.CustomPictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvLabels = new System.Windows.Forms.DataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsBtnDelete = new System.Windows.Forms.ToolStripButton();
            this.tsBtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splcMain)).BeginInit();
            this.splcMain.Panel1.SuspendLayout();
            this.splcMain.Panel2.SuspendLayout();
            this.splcMain.SuspendLayout();
            this.grbMainImg.SuspendLayout();
            this.pnlPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabels)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1265, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.tsLblCollectionName});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1265, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(122, 22);
            this.btnSave.Text = "Save to Collection";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tsLblCollectionName
            // 
            this.tsLblCollectionName.Name = "tsLblCollectionName";
            this.tsLblCollectionName.Size = new System.Drawing.Size(16, 22);
            this.tsLblCollectionName.Text = "...";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 661);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1265, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splcMain
            // 
            this.splcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splcMain.Location = new System.Drawing.Point(0, 49);
            this.splcMain.Name = "splcMain";
            // 
            // splcMain.Panel1
            // 
            this.splcMain.Panel1.Controls.Add(this.grbMainImg);
            // 
            // splcMain.Panel2
            // 
            this.splcMain.Panel2.Controls.Add(this.groupBox1);
            this.splcMain.Panel2.Controls.Add(this.toolStrip2);
            this.splcMain.Size = new System.Drawing.Size(1265, 612);
            this.splcMain.SplitterDistance = 856;
            this.splcMain.TabIndex = 3;
            // 
            // grbMainImg
            // 
            this.grbMainImg.Controls.Add(this.pnlPicture);
            this.grbMainImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbMainImg.Location = new System.Drawing.Point(0, 0);
            this.grbMainImg.Name = "grbMainImg";
            this.grbMainImg.Size = new System.Drawing.Size(856, 612);
            this.grbMainImg.TabIndex = 0;
            this.grbMainImg.TabStop = false;
            this.grbMainImg.Text = "Img";
            // 
            // pnlPicture
            // 
            this.pnlPicture.AutoScroll = true;
            this.pnlPicture.Controls.Add(this.picMain);
            this.pnlPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPicture.Location = new System.Drawing.Point(3, 16);
            this.pnlPicture.Name = "pnlPicture";
            this.pnlPicture.Size = new System.Drawing.Size(850, 593);
            this.pnlPicture.TabIndex = 1;
            // 
            // picMain
            // 
            this.picMain.BackColor = System.Drawing.SystemColors.Control;
            this.picMain.ErrorImage = global::RobotCloud.YoloCreatorDataTrain.Properties.Resources.ProfilePhoto;
            this.picMain.Image = global::RobotCloud.YoloCreatorDataTrain.Properties.Resources.ProfilePhoto;
            this.picMain.InitialImage = ((System.Drawing.Image)(resources.GetObject("picMain.InitialImage")));
            this.picMain.Location = new System.Drawing.Point(3, 3);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(448, 448);
            this.picMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picMain.TabIndex = 0;
            this.picMain.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvLabels);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 587);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Labled(s)";
            // 
            // dgvLabels
            // 
            this.dgvLabels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLabels.Location = new System.Drawing.Point(3, 16);
            this.dgvLabels.Name = "dgvLabels";
            this.dgvLabels.Size = new System.Drawing.Size(399, 568);
            this.dgvLabels.TabIndex = 0;
            this.dgvLabels.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLabels_CellClick);
            this.dgvLabels.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLabels_CellContentClick);
            this.dgvLabels.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLabels_CellEndEdit);
            this.dgvLabels.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLabels_CellEnter);
            this.dgvLabels.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLabels_CellLeave);
            this.dgvLabels.SelectionChanged += new System.EventHandler(this.dgvLabels_SelectionChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnDelete,
            this.tsBtnRefresh});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(405, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsBtnDelete
            // 
            this.tsBtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnDelete.Image")));
            this.tsBtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDelete.Name = "tsBtnDelete";
            this.tsBtnDelete.Size = new System.Drawing.Size(60, 22);
            this.tsBtnDelete.Text = "Delete";
            this.tsBtnDelete.Click += new System.EventHandler(this.tsBtnDelete_Click);
            // 
            // tsBtnRefresh
            // 
            this.tsBtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnRefresh.Image")));
            this.tsBtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRefresh.Name = "tsBtnRefresh";
            this.tsBtnRefresh.Size = new System.Drawing.Size(66, 22);
            this.tsBtnRefresh.Text = "Refresh";
            this.tsBtnRefresh.Click += new System.EventHandler(this.tsBtnRefresh_Click);
            // 
            // frmImageLabling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 683);
            this.Controls.Add(this.splcMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmImageLabling";
            this.Text = "Labling";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImageLabling_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splcMain.Panel1.ResumeLayout(false);
            this.splcMain.Panel2.ResumeLayout(false);
            this.splcMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splcMain)).EndInit();
            this.splcMain.ResumeLayout(false);
            this.grbMainImg.ResumeLayout(false);
            this.pnlPicture.ResumeLayout(false);
            this.pnlPicture.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabels)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splcMain;
        private System.Windows.Forms.GroupBox grbMainImg;
        private CustomPictureBox picMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.DataGridView dgvLabels;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton tsBtnDelete;
        private System.Windows.Forms.ToolStripButton tsBtnRefresh;
        private System.Windows.Forms.ToolStripLabel tsLblCollectionName;
        private System.Windows.Forms.Panel pnlPicture;
    }
}

