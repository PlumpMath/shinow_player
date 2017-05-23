using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ShinowPlayer
{
    public partial class FormChild : Form
    {
        public VlcPlayer vlc_player_;

        public int vlc_length = 0;

        public int video_length = 0;

        private bool is_playinig_;

        public FormChild()
        {
            InitializeComponent();

            this.vlc_player_ = new VlcPlayer(Service.PluginPath);
            IntPtr render_wnd = this.panel_player.Handle;
            this.vlc_player_.SetRenderWindow((int)render_wnd);
            this.is_playinig_ = false;
        }

        private void FormChild_Load(object sender, EventArgs e)
        {

        }

        private void FormChild_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.vlc_player_ != null)
            {
                this.vlc_player_.Stop();
                this.vlc_player_ = null;
            }
        }
    }
}
