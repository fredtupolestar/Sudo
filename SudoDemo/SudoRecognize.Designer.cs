namespace SudoDemo
{
    partial class SudoRecognize
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_load_image = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_process_gray = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_process_canny = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_process_ocr = new System.Windows.Forms.ToolStripMenuItem();
            this.pic_sudo = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_sudo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.processToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1513, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrl_load_image});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(56, 28);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // ctrl_load_image
            // 
            this.ctrl_load_image.Name = "ctrl_load_image";
            this.ctrl_load_image.Size = new System.Drawing.Size(211, 34);
            this.ctrl_load_image.Text = "Load Image";
            this.ctrl_load_image.Click += new System.EventHandler(this.ctrl_load_image_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrl_process_gray,
            this.ctrl_process_canny,
            this.ctrl_process_ocr});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(90, 28);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // ctrl_process_gray
            // 
            this.ctrl_process_gray.Name = "ctrl_process_gray";
            this.ctrl_process_gray.Size = new System.Drawing.Size(164, 34);
            this.ctrl_process_gray.Text = "Gray";
            this.ctrl_process_gray.Click += new System.EventHandler(this.ctrl_process_gray_Click);
            // 
            // ctrl_process_canny
            // 
            this.ctrl_process_canny.Name = "ctrl_process_canny";
            this.ctrl_process_canny.Size = new System.Drawing.Size(164, 34);
            this.ctrl_process_canny.Text = "Canny";
            this.ctrl_process_canny.Click += new System.EventHandler(this.ctrl_process_canny_Click);
            // 
            // ctrl_process_ocr
            // 
            this.ctrl_process_ocr.Name = "ctrl_process_ocr";
            this.ctrl_process_ocr.Size = new System.Drawing.Size(164, 34);
            this.ctrl_process_ocr.Text = "OCR";
            this.ctrl_process_ocr.Click += new System.EventHandler(this.ctrl_process_ocr_Click);
            // 
            // pic_sudo
            // 
            this.pic_sudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_sudo.Location = new System.Drawing.Point(0, 32);
            this.pic_sudo.Name = "pic_sudo";
            this.pic_sudo.Size = new System.Drawing.Size(1513, 1211);
            this.pic_sudo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pic_sudo.TabIndex = 1;
            this.pic_sudo.TabStop = false;
            // 
            // SudoRecognize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1513, 1243);
            this.Controls.Add(this.pic_sudo);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SudoRecognize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SudoRecognize";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SudoRecognize_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_sudo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem ctrl_load_image;
        private PictureBox pic_sudo;
        private ToolStripMenuItem processToolStripMenuItem;
        private ToolStripMenuItem ctrl_process_gray;
        private ToolStripMenuItem ctrl_process_canny;
        private ToolStripMenuItem ctrl_process_ocr;
    }
}