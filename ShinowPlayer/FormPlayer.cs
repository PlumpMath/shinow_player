using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ShinowPlayer
{
    public partial class FormPlayer : Form,IMessageFilter
    {
        public VlcPlayer vlc_player_;

        public int vlc_length = 0;

        public int video_length = 0;

        public int _vlcVolume = 50; //初始化大屏播放音量为最大值的一半the volume in percents (0 = mute, 100 = 0dB)

        //播放状态：0：停止，1：播放，2：暂停。
        public int is_playinig_;
        //大屏停止。
        public bool stopSmall = false;
        //小屏停止。
        public bool stopBig = false;

        //是否全屏模式。
        public bool isFullScreen = false;

        //是否是网络地址。
        public bool isUrl = false;

        //是否切换。
        public bool isExchange = false;

        public string firstPath = "";
        public string secondPath = "";

        public FormPlayer()
        {
            InitializeComponent();

            this.vlc_player_ = new VlcPlayer(Service.PluginPath);
            IntPtr render_wnd = this.panel_player.Handle;
            this.vlc_player_.SetRenderWindow((int)render_wnd);
            this.is_playinig_ = 0;
        }

        #region 开始按钮不要了
        /// <summary>
        /// 开始按钮。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_start_Click(object sender, EventArgs e)
        {
            Play();
        }

        #endregion

        /// <summary>
        /// 开始播放。
        /// </summary>
        public void Play()
        {
            this.button1.Tag = "暂停";
            this.button1.BackgroundImage = ShinowPlayer.Properties.Resources.pause;
            //调用父窗体的菜单栏 ,更改其文本和图标
            if ((this.MdiParent != null) && (this.MdiParent is FormMain))
            {
                (this.MdiParent as FormMain).暂停ToolStripMenuItem.Text = "暂停";
                (this.MdiParent as FormMain).暂停ToolStripMenuItem.Image = ShinowPlayer.Properties.Resources.pause;
            }

            if (Service.VideoList == null || Service.VideoList.Length == 0)
            {
                #region 测试专用-开始按钮

                //Service.VideoList = new string[] {"1.avi" ,"8.mp4" };
                // Service.VideoList = new string[] { "http://27.195.146.133/videos/other/20160302/0b/e8/2692463f584ad208201625b7767a5d52.f4v", "http://111.206.23.149/videos/other/20160226/3e/3f/62b7604eecebd869ca0a4401d306337f.f4v?src=iqiyi.com" };
                //网络地址播放黑屏,爬虫
                //http://www.iqiyi.com/v_19rrkxvdws.html?vfm=f_191_360y                           

                #endregion

                //若传入为空，则不播放。
                return;
            }

            firstPath = Service.VideoList[0];
            this.vlc_player_.Stop();
            Thread.Sleep(100);//延迟0.1秒
            this.vlc_player_.Play(firstPath, isUrl);
            Service.FormChild.Hide();
            this.vlc_length = (int)this.vlc_player_.Duration();
            this.video_length = Service.GetVideoLength(firstPath);

            if (this.vlc_length <= 0)
            {
                this.vlc_length = this.video_length;
            }
            if (this.video_length <= 0)
            {
                this.video_length = this.vlc_length;
            }
            if (Service.VideoList.Length > 1 && Service.FormChild != null)
            {               
                secondPath = Service.VideoList[1];
                Service.FormChild.vlc_player_.Stop();
                Thread.Sleep(100);//延迟0.1秒
                Service.FormChild.vlc_player_.Play(secondPath, isUrl);
                Service.FormChild.Show();
                this.button3.Show();

                Service.FormChild.vlc_length = (int)Service.FormChild.vlc_player_.Duration();
                Service.FormChild.video_length = Service.GetVideoLength(secondPath);

                if (Service.FormChild.vlc_length <= 0)
                {
                    Service.FormChild.vlc_length = Service.FormChild.video_length;
                }
                if (Service.FormChild.video_length <= 0)
                {
                    Service.FormChild.video_length = Service.FormChild.vlc_length;
                }
            }

            this.trackBar_process.SetRange(0, this.video_length);
            this.trackBar_process.Value = 0;
            this.timer_play.Start();
            this.is_playinig_ = 1;
            stopBig = false;
            stopSmall = false;
            isExchange = false;
            isFullScreen = false;

            this.vlc_player_.SetVolume(this._vlcVolume);
            this.trackBar_volume.Value = this._vlcVolume / (100 / this.trackBar_volume.Maximum);
            Service.FormChild.vlc_player_.SetVolume(0);//小屏视频启动时把音量设为静音.    
        }

        /// <summary>
        /// 计时器。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_play_Tick(object sender, EventArgs e)
        {
            if (this.is_playinig_ != 0)
            {
                if (Service.VideoList.Length == 0 || Service.VideoList.Length == 1)//读取本地文件，单屏显示或只传入一个文件
                {
                    if (this.trackBar_process.Value == this.trackBar_process.Maximum)
                    {
                        //播放完毕。
                        this.vlc_player_.Stop();
                        this.timer_play.Stop();
                        this.is_playinig_ = 0;
                        this.lbl_time.Text = "00:00:00 / 00:00:00";
                        this.trackBar_process.Value = 0;
                    }
                    else
                    {
                        this.trackBar_process.Value = this.trackBar_process.Value + 1;
                        this.lbl_time.Text = string.Format("{0}/{1}",
                            GetTimeString(this.trackBar_process.Value),
                            GetTimeString(this.trackBar_process.Maximum));
                    }
                }

                //大小屏
                if (Service.FormChild != null && Service.VideoList.Length > 1)
                {                 
                    if (this.trackBar_process.Value >= Service.FormChild.video_length)
                    {
                        Service.FormChild.vlc_player_.Stop();
                        Service.FormChild.Hide();
                        stopSmall = true;
                    }
                    if (this.trackBar_process.Value >= this.video_length)
                    {
                        this.vlc_player_.Stop();
                        stopBig = true;
                    }
                    if (stopSmall && stopBig)
                    {
                        this.timer_play.Stop();
                        this.is_playinig_ = 0;
                        this.lbl_time.Text = "00:00:00 / 00:00:00";
                        this.trackBar_process.Value = 0;
                    }
                    else if (stopBig)
                    {
                        this.lbl_time.Text = "00:00:00 / 00:00:00";
                        this.trackBar_process.Value = 0;
                        this.timer_play.Stop();
                    }
                    else
                    {
                        this.trackBar_process.Value = this.trackBar_process.Value + 1;
                        this.lbl_time.Text = string.Format("{0}/{1}",
                           GetTimeString(this.trackBar_process.Value),
                           GetTimeString(this.trackBar_process.Maximum));
                    }
                }
            }
        }

        public string GetTimeString(int val)
        {
            int hour = val / 3600;
            val %= 3600;
            int minute = val / 60;
            int second = val % 60;
            return string.Format("{0:00}:{1:00}:{2:00}", hour, minute, second);
        }

        /// <summary>
        /// 进度条。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar_process_Scroll(object sender, EventArgs e)
        {
            if (this.is_playinig_ != 0)
            {
                    float pos = (float)this.trackBar_process.Value / (float)this.video_length;
                    this.lbl_time.Text = string.Format("{0}/{1}",
                            GetTimeString(this.trackBar_process.Value),
                            GetTimeString(this.trackBar_process.Maximum));
                    this.vlc_player_.SetPlayPosition(pos);

                    if (Service.VideoList.Length > 1 && Service.FormChild != null)
                    {
                        //Service.FormChild.Show();
                        if (this.trackBar_process.Value >= Service.FormChild.video_length)
                        {
                            Service.FormChild.vlc_player_.Stop();
                            Service.FormChild.Hide();
                            stopSmall = true;
                        }
                        else
                        {
                            float cpos = (float)this.trackBar_process.Value / (float)Service.FormChild.video_length;
                            Service.FormChild.vlc_player_.SetPlayPosition(cpos);
                        }
                    }             
            }
        }

        /// <summary>
        /// 停止。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.vlc_player_ != null)
            {
                this.button1.Tag = "暂停";
                this.button1.BackgroundImage = ShinowPlayer.Properties.Resources.pause;
                //调用父窗体的菜单栏 ,更改其文本和图标
                if ((this.MdiParent != null) && (this.MdiParent is FormMain))
                {
                    (this.MdiParent as FormMain).暂停ToolStripMenuItem.Text = "暂停";
                    (this.MdiParent as FormMain).暂停ToolStripMenuItem.Image = ShinowPlayer.Properties.Resources.pause;
                }
                this.button3.Hide();
                this.vlc_player_.Stop();
                this.timer_play.Stop();
                this.trackBar_process.Value = 0;

                this.lbl_time.Text = "00:00:00 / 00:00:00";
                this.is_playinig_ = 0;

                if (Service.VideoList.Length > 1 && Service.FormChild != null)
                {
                    Service.FormChild.vlc_player_.Stop();
                    Service.FormChild.Hide();
                }
            }
        }

        private void panel_player_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 暂停。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.is_playinig_ == 0)
            {
                return;
            }
            if (this.vlc_player_ != null)
            {
                this.vlc_player_.Pause();
                if (Service.VideoList.Length > 1 && Service.FormChild != null)
                {
                    Service.FormChild.vlc_player_.Pause();
                    if (stopSmall)
                    {
                        Service.FormChild.Hide();
                    }
                    else
                    {
                        Service.FormChild.Show();
                    }
                }
                //控制时长与滚动条
                if (this.is_playinig_ == 1)
                {
                    this.timer_play.Stop();
                    this.is_playinig_ = 2;
                    this.button1.Tag = "继续";
                    this.button1.BackgroundImage = ShinowPlayer.Properties.Resources.start;
                    //调用父窗体的菜单栏 ,更改其文本和图标
                    if ((this.MdiParent != null) && (this.MdiParent is FormMain))
                    {
                        (this.MdiParent as FormMain).暂停ToolStripMenuItem.Text = "继续";
                        (this.MdiParent as FormMain).暂停ToolStripMenuItem.Image = ShinowPlayer.Properties.Resources.start;
                    }
                }
                else if (this.is_playinig_ == 2)
                {
                    this.timer_play.Start();
                    this.is_playinig_ = 1;
                    this.button1.Tag = "暂停";
                    this.button1.BackgroundImage = ShinowPlayer.Properties.Resources.pause;
                    //调用父窗体的菜单栏 ,更改其文本和图标
                    if ((this.MdiParent != null) && (this.MdiParent is FormMain))
                    {
                        (this.MdiParent as FormMain).暂停ToolStripMenuItem.Text = "暂停";
                        (this.MdiParent as FormMain).暂停ToolStripMenuItem.Image = ShinowPlayer.Properties.Resources.pause;
                    }
                }
                else
                {
                    this.trackBar_process.Value = 0;
                }
            }
        }

        /// <summary>
        /// 切换。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (is_playinig_ != 0 && Service.VideoList.Length > 1 && Service.FormChild != null)
            {
                if (stopBig)//表示大屏已播放完毕。
                {
                    Service.FormChild.Show();
                }
                else if (stopSmall)
                { }
                else
                {
                    Service.FormChild.Show();
                    isExchange = !isExchange;

                    var temp = this.video_length;
                    this.video_length = Service.FormChild.video_length;
                    Service.FormChild.video_length = temp;

                    float pos = (float)this.trackBar_process.Value / (float)this.video_length;
                    float cpos = (float)this.trackBar_process.Value / (float)Service.FormChild.video_length;

                    if (isExchange)
                    {
                        this.vlc_player_.Play(secondPath, isUrl);
                        Service.FormChild.vlc_player_.Play(firstPath, isUrl);                                          
                    }
                    else
                    {
                        this.vlc_player_.Play(firstPath, isUrl);
                        Service.FormChild.vlc_player_.Play(secondPath, isUrl);                                
                    }
                    this.vlc_player_.SetPlayPosition(pos);
                    Service.FormChild.vlc_player_.SetPlayPosition(cpos);

                    this.trackBar_process.SetRange(0, this.video_length);
                    this.lbl_time.Text = string.Format("{0}/{1}",
                       GetTimeString(this.trackBar_process.Value),
                       GetTimeString(this.trackBar_process.Maximum));

                    Thread.Sleep(350);//延迟0.1秒
                    if (button1.Tag.ToString() == @"继续")
                    {
                        //Service.FormChild.vlc_player_.Pause();
                        this.vlc_player_.Pause();
                        Thread.Sleep(200);
                        Service.FormChild.vlc_player_.Pause();
                    }

                }
            }
        }

        /// <summary>
        /// 滑块值改变时，修改音量值 。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar_volume_Scroll(object sender, EventArgs e)
        {
            if (this.vlc_player_ != null)
            {
                if (Service.VideoList.Length > 1 && Service.FormChild != null)
                {
                    if (stopSmall)
                    {
                        Service.FormChild.Hide();
                    }
                    else
                    {
                        Service.FormChild.Show();
                    }
                }
                this.vlc_player_.waveSetVolume(trackBar_volume.Value, trackBar_volume.Maximum, trackBar_volume.Minimum);

                //把当前小屏视频音量设为静音.
                if (Service.FormChild != null)
                {
                    Service.FormChild.vlc_player_.SetVolume(0);
                }
            }
        }

        /// <summary>
        /// 全屏方法。
        /// </summary>
        private void SetFullScreen()
        {
            if (is_playinig_ != 0)
            {
                isFullScreen = !isFullScreen;
                this.vlc_player_.SetFullScreen(this.panel_player, isFullScreen, Service.FormChild);

                if (Service.VideoList.Length > 1 && Service.FormChild != null && stopSmall == true)
                {
                    Service.FormChild.Hide();
                }
                else if (Service.VideoList.Length == 0 || Service.VideoList.Length == 1)
                {
                    Service.FormChild.Hide();
                }
            }
        }

        /// <summary>
        /// 全屏按钮。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            SetFullScreen();
        }

        /// <summary>
        /// 鼠标双击全屏。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hidePanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SetFullScreen();
        }

        /// <summary>
        /// 按esc键取消全屏。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPlayer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && isFullScreen)
            {
                isFullScreen = false;
                e.Handled = true;
                this.vlc_player_.SetFullScreen(this.panel_player, isFullScreen, Service.FormChild);
            }
            if (Service.VideoList.Length > 1 && Service.FormChild != null && stopSmall == true)
            {
                Service.FormChild.Hide();
            }
            else if (Service.VideoList.Length == 0 || Service.VideoList.Length == 1)
            {
                Service.FormChild.Hide();
            }
        }

        /// <summary>
        /// 鼠标单击进度条。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar_process_MouseUp(object sender, MouseEventArgs e)
        {
            var trackX = this.trackBar_process.Size.Width;
            var mouseX = e.X;
            var newValue =Convert.ToInt32( Math.Ceiling( (double)mouseX*this.trackBar_process.Maximum/trackX));
            newValue = newValue >= this.trackBar_process.Maximum ? this.trackBar_process.Maximum : newValue;
            if (!stopBig)
            {
                this.trackBar_process.Value = newValue;
                float pos = (float)this.trackBar_process.Value / (float)this.video_length;
                this.vlc_player_.SetPlayPosition(pos);
            }
            if (!stopSmall)
            {
                if (!stopBig)
                {
                    float cpos = (float)this.trackBar_process.Value / (float)Service.FormChild.video_length;
                    Service.FormChild.vlc_player_.SetPlayPosition(cpos);
                }
            }
        }

        private void FormPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.vlc_player_ != null)
            {
                this.vlc_player_.Stop();
                this.vlc_player_ = null;
            }
        }

        /// <summary>
        /// 禁用鼠标滑轮带动视频播放。
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 522)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void FormPlayer_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);
        }
    }
}
