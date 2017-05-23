namespace ShinowPlayer
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.播放ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.暂停ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.切换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全屏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.音频ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.增大音量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.减小音量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.靜音ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.播放ToolStripMenuItem,
            this.音频ToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(584, 25);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Image = global::ShinowPlayer.Properties.Resources.start;
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Image = global::ShinowPlayer.Properties.Resources.exit;
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 播放ToolStripMenuItem
            // 
            this.播放ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.暂停ToolStripMenuItem,
            this.停止ToolStripMenuItem,
            this.切换ToolStripMenuItem,
            this.全屏ToolStripMenuItem});
            this.播放ToolStripMenuItem.Name = "播放ToolStripMenuItem";
            this.播放ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.播放ToolStripMenuItem.Text = "播放";
            // 
            // 暂停ToolStripMenuItem
            // 
            this.暂停ToolStripMenuItem.Image = global::ShinowPlayer.Properties.Resources.pause;
            this.暂停ToolStripMenuItem.Name = "暂停ToolStripMenuItem";
            this.暂停ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.暂停ToolStripMenuItem.Text = "暂停";
            this.暂停ToolStripMenuItem.Click += new System.EventHandler(this.暂停ToolStripMenuItem_Click);
            // 
            // 停止ToolStripMenuItem
            // 
            this.停止ToolStripMenuItem.Image = global::ShinowPlayer.Properties.Resources.stop;
            this.停止ToolStripMenuItem.Name = "停止ToolStripMenuItem";
            this.停止ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.停止ToolStripMenuItem.Text = "停止";
            this.停止ToolStripMenuItem.Click += new System.EventHandler(this.停止ToolStripMenuItem_Click);
            // 
            // 切换ToolStripMenuItem
            // 
            this.切换ToolStripMenuItem.Image = global::ShinowPlayer.Properties.Resources.Switch;
            this.切换ToolStripMenuItem.Name = "切换ToolStripMenuItem";
            this.切换ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.切换ToolStripMenuItem.Text = "切换";
            this.切换ToolStripMenuItem.Click += new System.EventHandler(this.切换ToolStripMenuItem_Click);
            // 
            // 全屏ToolStripMenuItem
            // 
            this.全屏ToolStripMenuItem.Image = global::ShinowPlayer.Properties.Resources.screen_ful;
            this.全屏ToolStripMenuItem.Name = "全屏ToolStripMenuItem";
            this.全屏ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.全屏ToolStripMenuItem.Text = "全屏";
            this.全屏ToolStripMenuItem.Click += new System.EventHandler(this.全屏ToolStripMenuItem_Click);
            // 
            // 音频ToolStripMenuItem
            // 
            this.音频ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.增大音量ToolStripMenuItem,
            this.减小音量ToolStripMenuItem,
            this.靜音ToolStripMenuItem});
            this.音频ToolStripMenuItem.Name = "音频ToolStripMenuItem";
            this.音频ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.音频ToolStripMenuItem.Text = "音频";
            // 
            // 增大音量ToolStripMenuItem
            // 
            this.增大音量ToolStripMenuItem.Image = global::ShinowPlayer.Properties.Resources.vb;
            this.增大音量ToolStripMenuItem.Name = "增大音量ToolStripMenuItem";
            this.增大音量ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.增大音量ToolStripMenuItem.Text = "增大音量";
            this.增大音量ToolStripMenuItem.Click += new System.EventHandler(this.增大音量ToolStripMenuItem_Click);
            // 
            // 减小音量ToolStripMenuItem
            // 
            this.减小音量ToolStripMenuItem.Image = global::ShinowPlayer.Properties.Resources.vs;
            this.减小音量ToolStripMenuItem.Name = "减小音量ToolStripMenuItem";
            this.减小音量ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.减小音量ToolStripMenuItem.Text = "减小音量";
            this.减小音量ToolStripMenuItem.Click += new System.EventHandler(this.减小音量ToolStripMenuItem_Click);
            // 
            // 靜音ToolStripMenuItem
            // 
            this.靜音ToolStripMenuItem.Image = global::ShinowPlayer.Properties.Resources.v0;
            this.靜音ToolStripMenuItem.Name = "靜音ToolStripMenuItem";
            this.靜音ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.靜音ToolStripMenuItem.Text = "靜音";
            this.靜音ToolStripMenuItem.Click += new System.EventHandler(this.靜音ToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.menuStripMain);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStripMain;
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShinowPlayer V1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Move += new System.EventHandler(this.FormMain_Move);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 播放ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 音频ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 增大音量ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 减小音量ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 靜音ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 切换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全屏ToolStripMenuItem;
        public System.Windows.Forms.MenuStrip menuStripMain;
        public System.Windows.Forms.ToolStripMenuItem 暂停ToolStripMenuItem;
    }
}

