using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ShinowPlayer
{
    public partial class FormMain : Form
    {
       public int initx = 0;//主窗体初始left距离
       public int inity = 0;//主窗体初始top距离
       public int initx1 = 0;//子窗体初始left距离
       public int inity1 = 0;//子窗体初始top距离
        public FormMain(string[] args,VideoType vt)
        {
            InitializeComponent();
            Service.VideoList = args;
            Service.FormPlayer = new FormPlayer();
            Service.FormChild = new FormChild();
            Service.FormPlayer.isUrl = (vt == VideoType.Url ? true : false);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Service.FormPlayer.MdiParent = this;
            Service.FormPlayer.Show();

            //初始化时小屏和切换按钮不显示。只有传入两个视频参数时才显示。
            Service.FormChild.Owner = this;
            Service.FormChild.ShowInTaskbar = false;
            Service.FormChild.TopMost = true;
            //Service.FormChild.Show();
            //Service.FormChild.Hide();
            Service.FormPlayer.button3.Hide();           

            this.setSize();
            //加载完就开始播放
            Service.FormPlayer.Play();
            Service.FormPlayer.btn_start.Hide();//开始按钮不要
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            this.setSize();
        }

        public void setSize()
        { 
           Service.FormPlayer.Size = new Size(this.Size.Width - 20, this.Size.Height-68);

            int childWidth = Service.FormPlayer.Size.Width / 4;
            int childHeight = childWidth * 3 / 4;

            Service.FormChild.Size = new Size(childWidth, childHeight);
            Service.FormChild.StartPosition = FormStartPosition.Manual;
           // Service.FormChild.Location = new Point(Service.FormPlayer.Size.Width - childWidth+2, Service.FormPlayer.Size.Height - childHeight-26 );
            Service.FormChild.Top = this.Top + this.Size.Height - childHeight-86;
            Service.FormChild.Left = this.Left + this.Size.Width - childWidth-10;

            initx = this.Left;
            inity = this.Top;
            initx1 = Service.FormChild.Left;
            inity1 = Service.FormChild.Top;
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {         
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.*)|*.*";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            Service.FormPlayer.button1.Tag = "暂停";
            Service.FormPlayer.button1.BackgroundImage = ShinowPlayer.Properties.Resources.pause;
            暂停ToolStripMenuItem.Text = "暂停";
            暂停ToolStripMenuItem.Image = ShinowPlayer.Properties.Resources.pause;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Service.FormPlayer.vlc_player_.Stop();
                Service.FormChild.vlc_player_.Stop();
                Thread.Sleep(100);//延迟0.1秒

                Service.FormPlayer.vlc_player_.Play(ofd.FileName, false);
                Service.FormPlayer.vlc_length = (int)Service.FormPlayer.vlc_player_.Duration();
                Service.FormPlayer.video_length = Service.GetVideoLength(ofd.FileName);

                if (Service.FormPlayer.vlc_length <= 0)
                {
                    Service.FormPlayer.vlc_length = Service.FormPlayer.video_length;
                }

                if (Service.FormPlayer.video_length <= 0)
                {
                    Service.FormPlayer.video_length = Service.FormPlayer.vlc_length;
                }

                Service.FormPlayer.trackBar_process.SetRange(0, Service.FormPlayer.video_length);
                Service.FormPlayer.trackBar_process.Value = 0;
                Service.FormPlayer.timer_play.Start();
                Service.FormPlayer.is_playinig_ = 1;
                Service.FormPlayer.stopBig = false;
                Service.FormPlayer.stopSmall = false;
                Service.FormPlayer.isExchange = false;
                Service.FormPlayer.isFullScreen = false;
                Service.FormPlayer.isUrl = false;
                Service.FormPlayer.vlc_player_.SetVolume(Service.FormPlayer._vlcVolume);
                Service.FormPlayer.trackBar_volume.Value = Service.FormPlayer._vlcVolume/(100 / Service.FormPlayer.trackBar_volume.Maximum);
                Service.FormChild.Hide();
                Service.VideoList = new string[] { };
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void 增大音量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Service.FormPlayer.vlc_player_ != null)
            {
                Service.FormPlayer.vlc_player_.waveSetVolume(((Service.FormPlayer.trackBar_volume.Value + 1) > 20 ? 20 : Service.FormPlayer.trackBar_volume.Value + 1), Service.FormPlayer.trackBar_volume.Maximum, Service.FormPlayer.trackBar_volume.Minimum);
                Service.FormPlayer.trackBar_volume.Value = Service.FormPlayer.trackBar_volume.Value + 1 > 20 ? 20 : Service.FormPlayer.trackBar_volume.Value + 1;              
            }     
        }

        private void 减小音量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Service.FormPlayer.vlc_player_ != null)
            {
                Service.FormPlayer.vlc_player_.waveSetVolume(((Service.FormPlayer.trackBar_volume.Value - 1) < 0 ? 0 : Service.FormPlayer.trackBar_volume.Value - 1), Service.FormPlayer.trackBar_volume.Maximum, Service.FormPlayer.trackBar_volume.Minimum);
                Service.FormPlayer.trackBar_volume.Value = Service.FormPlayer.trackBar_volume.Value - 1 < 0 ? 0 : Service.FormPlayer.trackBar_volume.Value - 1;            
            }   
        }

        private void 靜音ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Service.FormPlayer.vlc_player_.waveSetVolume(0, Service.FormPlayer.trackBar_volume.Maximum, Service.FormPlayer.trackBar_volume.Minimum);
            Service.FormPlayer.trackBar_volume.Value = 0;
        }

        private void 全屏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Service.FormPlayer.is_playinig_!=0)
            {
                Service.FormPlayer.isFullScreen = !Service.FormPlayer.isFullScreen;
                Service.FormPlayer.vlc_player_.SetFullScreen(Service.FormPlayer.panel_player, Service.FormPlayer.isFullScreen, Service.FormChild);

                if (Service.VideoList.Length > 1 && Service.FormChild != null && Service.FormPlayer.stopSmall == true)
                {
                    Service.FormChild.Hide();
                }
                else if (Service.VideoList.Length == 0 || Service.VideoList.Length == 1)
                {
                    Service.FormChild.Hide();
                }   
            }             
        }

        private void 暂停ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Service.FormPlayer.vlc_player_ != null)
            {
                Service.FormPlayer.vlc_player_.Pause();

                if (Service.VideoList.Length > 1 && Service.FormChild != null)
                {
                    Service.FormChild.vlc_player_.Pause();
                    if (Service.FormPlayer.stopSmall)
                    {
                        Service.FormChild.Hide();
                    }
                    else
                    {
                        Service.FormChild.Show();
                    }
                }

                //控制时长与滚动条
                if (Service.FormPlayer.is_playinig_ == 1)
                {
                    Service.FormPlayer.timer_play.Stop();
                    Service.FormPlayer.is_playinig_ = 2;
                    Service.FormPlayer.button1.Tag = "继续";
                    Service.FormPlayer.button1.BackgroundImage = ShinowPlayer.Properties.Resources.start;
                    暂停ToolStripMenuItem.Text = "继续";
                    暂停ToolStripMenuItem.Image = ShinowPlayer.Properties.Resources.start;
                }
                else if (Service.FormPlayer.is_playinig_ == 2)
                {
                    Service.FormPlayer.timer_play.Start();
                    Service.FormPlayer.is_playinig_ = 1;
                    Service.FormPlayer.button1.Tag = "暂停";
                    Service.FormPlayer.button1.BackgroundImage = ShinowPlayer.Properties.Resources.pause;
                    暂停ToolStripMenuItem.Text = "暂停";
                    暂停ToolStripMenuItem.Image = ShinowPlayer.Properties.Resources.pause;
                }
                else
                {
                    Service.FormPlayer.trackBar_process.Value = 0;
                }
            } 
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Service.FormPlayer.vlc_player_ != null)
            {
                Service.FormPlayer.button1.Tag = "暂停";
                Service.FormPlayer.button1.BackgroundImage = ShinowPlayer.Properties.Resources.pause;
                暂停ToolStripMenuItem.Text = "暂停";
                暂停ToolStripMenuItem.Image = ShinowPlayer.Properties.Resources.pause;
                Service.FormPlayer.button3.Hide();
                Service.FormPlayer.vlc_player_.Stop();
                Service.FormPlayer.timer_play.Stop();
                Service.FormPlayer.trackBar_process.Value = 0;
                Service.FormPlayer.lbl_time.Text = "00:00:00 / 00:00:00";
                Service.FormPlayer.is_playinig_ = 0;

                if (Service.VideoList.Length > 1 && Service.FormChild != null)
                {
                    Service.FormChild.vlc_player_.Stop();
                    Service.FormChild.Hide();
                }
            }
        }  

        private void 切换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Service.FormPlayer.is_playinig_ != 0 && Service.VideoList.Length > 1 && Service.FormChild != null)
            {
                if (Service.FormPlayer.stopBig)//表示大屏已播放完毕。
                {
                    Service.FormChild.Show();
                }
                else if (Service.FormPlayer.stopSmall)
                { }
                else
                {
                    Service.FormChild.Show();
                    Service.FormPlayer.isExchange = !Service.FormPlayer.isExchange;

                    var temp = Service.FormPlayer.video_length;
                    Service.FormPlayer.video_length = Service.FormChild.video_length;
                    Service.FormChild.video_length = temp;

                    float pos = (float)Service.FormPlayer.trackBar_process.Value / (float)Service.FormPlayer.video_length;
                    float cpos = (float)Service.FormPlayer.trackBar_process.Value / (float)Service.FormChild.video_length;

                    if (Service.FormPlayer.isExchange)
                    {
                        Service.FormPlayer.vlc_player_.Play(Service.FormPlayer.secondPath, Service.FormPlayer.isUrl);
                        Service.FormChild.vlc_player_.Play(Service.FormPlayer.firstPath, Service.FormPlayer.isUrl);                      
                    }
                    else
                    {
                        Service.FormPlayer.vlc_player_.Play(Service.FormPlayer.firstPath, Service.FormPlayer.isUrl);
                        Service.FormChild.vlc_player_.Play(Service.FormPlayer.secondPath, Service.FormPlayer.isUrl);                       
                    }

                    Service.FormPlayer.vlc_player_.SetPlayPosition(pos);
                    Service.FormChild.vlc_player_.SetPlayPosition(cpos);

                    Service.FormPlayer.trackBar_process.SetRange(0, Service.FormPlayer.video_length);
                    Service.FormPlayer.lbl_time.Text = string.Format("{0}/{1}",
                       Service.FormPlayer.GetTimeString(Service.FormPlayer.trackBar_process.Value),
                       Service.FormPlayer.GetTimeString(Service.FormPlayer.trackBar_process.Maximum));
                   

                    Thread.Sleep(350);
                    if (Service.FormPlayer.button1.Tag.ToString() == @"继续")
                    {
                       // Service.FormChild.vlc_player_.Pause();
                        Service.FormPlayer.vlc_player_.Pause();

                        Thread.Sleep(200);
                        Service.FormChild.vlc_player_.Pause();
                    }
                }
            }
        }

        private void FormMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("ok");
        }

        /// <summary>
        /// 主窗体移动，子窗体跟随。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Move(object sender, EventArgs e)
        {
            if (Service.FormChild != null)
            {               
                Service.FormChild.Top = inity1 + (this.Top - inity);
                Service.FormChild.Left = initx1 + (this.Left - initx);
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
        }       

    }
}
