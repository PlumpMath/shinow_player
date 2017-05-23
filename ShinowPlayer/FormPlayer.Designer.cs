namespace ShinowPlayer
{
    partial class FormPlayer
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
            this.panel_player = new System.Windows.Forms.Panel();
            this.hidePanel = new System.Windows.Forms.Panel();
            this.trackBar_process = new System.Windows.Forms.TrackBar();
            this.btn_start = new System.Windows.Forms.Button();
            this.lbl_time = new System.Windows.Forms.Label();
            this.trackBar_volume = new System.Windows.Forms.TrackBar();
            this.timer_play = new System.Windows.Forms.Timer(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel_player.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_process)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volume)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_player
            // 
            this.panel_player.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_player.BackColor = System.Drawing.Color.Black;
            this.panel_player.Controls.Add(this.hidePanel);
            this.panel_player.Location = new System.Drawing.Point(0, 0);
            this.panel_player.Name = "panel_player";
            this.panel_player.Size = new System.Drawing.Size(500, 325);
            this.panel_player.TabIndex = 0;
            this.panel_player.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_player_Paint);
            // 
            // hidePanel
            // 
            this.hidePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hidePanel.Location = new System.Drawing.Point(0, 0);
            this.hidePanel.Name = "hidePanel";
            this.hidePanel.Size = new System.Drawing.Size(500, 325);
            this.hidePanel.TabIndex = 1;
            this.hidePanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.hidePanel_MouseDoubleClick);
            // 
            // trackBar_process
            // 
            this.trackBar_process.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_process.AutoSize = false;
            this.trackBar_process.LargeChange = 1;
            this.trackBar_process.Location = new System.Drawing.Point(0, 331);
            this.trackBar_process.Name = "trackBar_process";
            this.trackBar_process.Size = new System.Drawing.Size(370, 20);
            this.trackBar_process.TabIndex = 1;
            this.trackBar_process.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_process.Scroll += new System.EventHandler(this.trackBar_process_Scroll);
            this.trackBar_process.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_process_MouseUp);
            // 
            // btn_start
            // 
            this.btn_start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_start.Location = new System.Drawing.Point(231, 368);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(52, 23);
            this.btn_start.TabIndex = 2;
            this.btn_start.Text = "开始";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // lbl_time
            // 
            this.lbl_time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_time.AutoSize = true;
            this.lbl_time.Location = new System.Drawing.Point(376, 335);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(119, 12);
            this.lbl_time.TabIndex = 5;
            this.lbl_time.Text = "00:00:00 / 00:00:00";
            // 
            // trackBar_volume
            // 
            this.trackBar_volume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_volume.AutoSize = false;
            this.trackBar_volume.LargeChange = 1;
            this.trackBar_volume.Location = new System.Drawing.Point(297, 363);
            this.trackBar_volume.Maximum = 20;
            this.trackBar_volume.Name = "trackBar_volume";
            this.trackBar_volume.Size = new System.Drawing.Size(191, 25);
            this.trackBar_volume.TabIndex = 6;
            this.trackBar_volume.Scroll += new System.EventHandler(this.trackBar_volume_Scroll);
            // 
            // timer_play
            // 
            this.timer_play.Interval = 1000;
            this.timer_play.Tick += new System.EventHandler(this.timer_play_Tick);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.BackgroundImage = global::ShinowPlayer.Properties.Resources.screen_ful;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Location = new System.Drawing.Point(97, 356);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(35, 35);
            this.button4.TabIndex = 8;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.BackgroundImage = global::ShinowPlayer.Properties.Resources.Switch;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Location = new System.Drawing.Point(140, 356);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(35, 35);
            this.button3.TabIndex = 7;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.BackgroundImage = global::ShinowPlayer.Properties.Resources.stop;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Location = new System.Drawing.Point(55, 356);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 35);
            this.button2.TabIndex = 4;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackgroundImage = global::ShinowPlayer.Properties.Resources.pause;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(13, 356);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 35);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.ControlBox = false;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.trackBar_volume);
            this.Controls.Add(this.lbl_time);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.trackBar_process);
            this.Controls.Add(this.panel_player);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPlayer";
            this.Text = "FormPlay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPlayer_FormClosing);
            this.Load += new System.EventHandler(this.FormPlayer_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormPlayer_KeyUp);
            this.panel_player.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_process)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TrackBar trackBar_process;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.Timer timer_play;
        public System.Windows.Forms.TrackBar trackBar_volume;
        public System.Windows.Forms.Panel panel_player;
        public System.Windows.Forms.Label lbl_time;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel hidePanel;
        public System.Windows.Forms.Button btn_start;

    }
}