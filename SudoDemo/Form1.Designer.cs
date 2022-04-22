namespace SudoDemo;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctl_new_game = new System.Windows.Forms.ToolStripMenuItem();
            this.ctl_exit_game = new System.Windows.Forms.ToolStripMenuItem();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_level_low = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_level_middle = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_level_high = new System.Windows.Forms.ToolStripMenuItem();
            this.recognizeSudoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrl_resolver = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.levelToolStripMenuItem,
            this.recognizeSudoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(830, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctl_new_game,
            this.ctl_exit_game});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(76, 28);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // ctl_new_game
            // 
            this.ctl_new_game.Name = "ctl_new_game";
            this.ctl_new_game.Size = new System.Drawing.Size(176, 34);
            this.ctl_new_game.Text = "New(&N)";
            this.ctl_new_game.Click += new System.EventHandler(this.ctl_new_game_Click);
            // 
            // ctl_exit_game
            // 
            this.ctl_exit_game.Name = "ctl_exit_game";
            this.ctl_exit_game.Size = new System.Drawing.Size(176, 34);
            this.ctl_exit_game.Text = "Exit(&E)";
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrl_level_low,
            this.ctrl_level_middle,
            this.ctrl_level_high});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(69, 28);
            this.levelToolStripMenuItem.Text = "Level";
            // 
            // ctrl_level_low
            // 
            this.ctrl_level_low.Name = "ctrl_level_low";
            this.ctrl_level_low.Size = new System.Drawing.Size(202, 34);
            this.ctrl_level_low.Text = "Low(&L)";
            this.ctrl_level_low.Click += new System.EventHandler(this.ctrl_level_low_Click);
            // 
            // ctrl_level_middle
            // 
            this.ctrl_level_middle.Name = "ctrl_level_middle";
            this.ctrl_level_middle.Size = new System.Drawing.Size(202, 34);
            this.ctrl_level_middle.Text = "Middle(&M)";
            this.ctrl_level_middle.Click += new System.EventHandler(this.ctrl_level_middle_Click);
            // 
            // ctrl_level_high
            // 
            this.ctrl_level_high.Name = "ctrl_level_high";
            this.ctrl_level_high.Size = new System.Drawing.Size(202, 34);
            this.ctrl_level_high.Text = "High(&H)";
            this.ctrl_level_high.Click += new System.EventHandler(this.ctrl_level_high_Click);
            // 
            // recognizeSudoToolStripMenuItem
            // 
            this.recognizeSudoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrl_resolver});
            this.recognizeSudoToolStripMenuItem.Name = "recognizeSudoToolStripMenuItem";
            this.recognizeSudoToolStripMenuItem.Size = new System.Drawing.Size(91, 28);
            this.recognizeSudoToolStripMenuItem.Text = "Resolve";
            // 
            // ctrl_resolver
            // 
            this.ctrl_resolver.Name = "ctrl_resolver";
            this.ctrl_resolver.Size = new System.Drawing.Size(206, 34);
            this.ctrl_resolver.Text = "Resolver(&R)";
            this.ctrl_resolver.Click += new System.EventHandler(this.ctrl_resolver_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(10, 40);
            this.panel1.Margin = new System.Windows.Forms.Padding(10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(810, 810);
            this.panel1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 862);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sudo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem gameToolStripMenuItem;
    private ToolStripMenuItem ctl_new_game;
    private ToolStripMenuItem ctl_exit_game;
    private Panel panel1;
    private ToolStripMenuItem levelToolStripMenuItem;
    private ToolStripMenuItem ctrl_level_low;
    private ToolStripMenuItem ctrl_level_middle;
    private ToolStripMenuItem ctrl_level_high;
    private ToolStripMenuItem recognizeSudoToolStripMenuItem;
    private ToolStripMenuItem ctrl_resolver;
}
