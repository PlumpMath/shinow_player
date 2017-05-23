using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace ShinowPlayer
{
    public class VlcPlayer
    {
        private IntPtr libvlc_instance_;
        private IntPtr libvlc_media_player_;

        private double duration_;

        public VlcPlayer(string pluginPath)
        {
            string plugin_arg = "--plugin-path=" + pluginPath;
            string[] arguments = { "-I", "dummy", "--ignore-config", "--no-video-title", plugin_arg };
            libvlc_instance_ = LibVlcAPI.libvlc_new(arguments);

            libvlc_media_player_ = LibVlcAPI.libvlc_media_player_new(libvlc_instance_);
        }

        public void SetRenderWindow(int wndHandle)
        {
            if (libvlc_instance_ != IntPtr.Zero && wndHandle != 0)
            {
                LibVlcAPI.libvlc_media_player_set_hwnd(libvlc_media_player_, wndHandle);
            }
        }

        /*
        public void PlayFile(string filePath)
        {
            IntPtr libvlc_media = LibVlcAPI.libvlc_media_new_path(libvlc_instance_, filePath);
            if (libvlc_media != IntPtr.Zero)
            {
                LibVlcAPI.libvlc_media_parse(libvlc_media);
                //LibVlcAPI.libvlc_media_parse_async(libvlc_media);


                LibVlcAPI.libvlc_media_player_set_media(libvlc_media_player_, libvlc_media);
                LibVlcAPI.libvlc_media_release(libvlc_media);

                LibVlcAPI.libvlc_media_player_play(libvlc_media_player_);
                Thread.Sleep(10);

                duration_ = LibVlcAPI.libvlc_media_get_duration(libvlc_media) / 1000.0;

                //duration_ = duration_ <= 0 ? 9999 : duration_;
            }
        }

        public void PlayUrl(string url)
        {
            IntPtr libvlc_media = LibVlcAPI.libvlc_media_new_location(libvlc_instance_, url);
            if (libvlc_media != IntPtr.Zero)
            {
                LibVlcAPI.libvlc_media_parse(libvlc_media);           

                LibVlcAPI.libvlc_media_player_set_media(libvlc_media_player_, libvlc_media);
                LibVlcAPI.libvlc_media_release(libvlc_media);

                LibVlcAPI.libvlc_media_player_play(libvlc_media_player_);
                Thread.Sleep(10);

                duration_ = LibVlcAPI.libvlc_media_get_duration(libvlc_media) / 1000.0;

            }
        }
        */
         
        public void Play(string ppath, bool isUrl)
        {
            IntPtr libvlc_media;
            if (isUrl)
            {
                 libvlc_media = LibVlcAPI.libvlc_media_new_location(libvlc_instance_, ppath);
            }
            else
            {
                 libvlc_media = LibVlcAPI.libvlc_media_new_path(libvlc_instance_, ppath);
            }
            if (libvlc_media != IntPtr.Zero)
            {
                LibVlcAPI.libvlc_media_parse(libvlc_media);

                LibVlcAPI.libvlc_media_player_set_media(libvlc_media_player_, libvlc_media);
                LibVlcAPI.libvlc_media_release(libvlc_media);

                LibVlcAPI.libvlc_media_player_play(libvlc_media_player_);
                Thread.Sleep(10);

                duration_ = LibVlcAPI.libvlc_media_get_duration(libvlc_media) / 1000.0;

            }

        }


        public void Pause()
        {
            if (libvlc_media_player_ != IntPtr.Zero)
            {
                LibVlcAPI.libvlc_media_player_pause(libvlc_media_player_);
            }
        }

        public void Stop()
        {
            if (libvlc_media_player_ != IntPtr.Zero)
            {
                LibVlcAPI.libvlc_media_player_stop(libvlc_media_player_);
            }
        }

        public double GetPlayTime()
         {
            return LibVlcAPI.libvlc_media_player_get_time(libvlc_media_player_) / 1000.0;
           }

        public void SetPlayTime(double seekTime)
        {
            LibVlcAPI.libvlc_media_player_set_time(libvlc_media_player_, (Int64)(seekTime * 1000));
        }

        public void SetPlayPosition(float pos)
        {
            LibVlcAPI.libvlc_media_player_set_position(libvlc_media_player_, pos);
        }

        public int GetVolume()
        {
            return LibVlcAPI.libvlc_audio_get_volume(libvlc_media_player_);
        }

        public void SetVolume(int volume)
        {
            LibVlcAPI.libvlc_audio_set_volume(libvlc_media_player_, volume);
        }

        public void SetFullScreen(bool istrue)
        {
            LibVlcAPI.libvlc_set_fullscreen(libvlc_media_player_, istrue ? 1 : 0);
        }

        public double Duration()
        {
            return duration_;
        }

        public string Version()
        {
            return LibVlcAPI.libvlc_get_version();
        }

        /// <summary>
        /// 设置音量。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxValue"></param>
        /// <param name="minValue"></param>
        public void waveSetVolume(int value,int maxValue,int minValue)
        {
            UInt32 Value = (System.UInt32)((double)0xffff * (double)value / (double)(maxValue - minValue));//先把trackbar的value值映射到0x0000～0xFFFF范围     
            //限制value的取值范围    
            if (Value < 0) Value = 0;
            if (Value > 0xffff) Value = 0xffff;
            UInt32 left = (System.UInt32)Value;//左声道音量    
            UInt32 right = (System.UInt32)Value;//右    
            LibVlcAPI.waveOutSetVolume(0, left << 16 | right); //"<<"左移，“|”逻辑或运算    
        }

        /*
        /// <summary>
        /// 获取音量。
        /// </summary>
        /// <param name="maxValue"></param>
        /// <param name="minValue"></param>
        /// <returns></returns>
        public int waveGetVolume(int maxValue,int minValue)
        {
            UInt32 d, v;
            d = 0;
            long i = LibVlcAPI.waveOutGetVolume(d, out v);
            UInt32 vleft = v & 0xFFFF;
            UInt32 vright = (v & 0xFFFF0000) >> 16;

            int value=(int.Parse(vleft.ToString()) | int.Parse(vright.ToString())) * (maxValue - minValue) / 0xFFFF;

            return value;
        
        }   
        */

        /// <summary>
        /// 设置播放器全屏。
        /// </summary>
        /// <param name="control"></param>
        public void SetFullScreen(Control control, bool isFullScreen, Control control2=null)
        { 
            //获取任务栏的句柄
           //IntPtr trayHwnd = LibVlcAPI.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", null);
           IntPtr trayHwnd = LibVlcAPI.FindWindow("Shell_TrayWnd", null);        
            //获取开始按钮句柄
           IntPtr startHwnd = LibVlcAPI.FindWindow("Button", null);
       
          if (isFullScreen)//全屏
            {
              //隐藏任务栏和开始按钮
                if (trayHwnd != IntPtr.Zero)
                {
                    LibVlcAPI.ShowWindow(trayHwnd, LibVlcAPI.SW_HIDE); 
                }
                if (startHwnd != IntPtr.Zero)
                {
                    LibVlcAPI.ShowWindow(startHwnd, LibVlcAPI.SW_HIDE);
                }
                control.Location = new Point(0, 0);
                control.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
               
                if (control2 != null)
                {
                    int childWidth = control.Size.Width / 4;
                    int childHeight = childWidth * 3 / 4;
                    control2.Size = new Size(childWidth, childHeight);
                    control2.Location = new Point(control.Size.Width - childWidth, control.Size.Height - childHeight );
                    control2.Show();
                  //  LibVlcAPI.SetParent(control2.Handle, IntPtr.Zero);
                    
                }
                control.Focus();// 获得焦点，否则也得不到按键
                LibVlcAPI.SetParent(control.Handle, IntPtr.Zero);                   
            }
            else
            {
              //显示任务栏和开始按钮
                if (trayHwnd != IntPtr.Zero)
                {
                    LibVlcAPI.ShowWindow(trayHwnd, LibVlcAPI.SW_SHOW);
                }
                if (startHwnd != IntPtr.Zero)
                {
                    LibVlcAPI.ShowWindow(startHwnd, LibVlcAPI.SW_SHOW);
                }
                control.Left = 2;
                control.Top = 0;
                control.Size = new Size(control.Parent.Width , control.Parent.Height-75 );
                
                if (control2 != null)
                {
                    int childWidth = control.Size.Width / 4;
                    int childHeight = childWidth * 3 / 4;
                    control2.Size = new Size(childWidth, childHeight);
                   // control2.Location = new Point(control.Size.Width - childWidth+2, control.Size.Height - childHeight+48 );
                    control2.Top = control.Parent.Parent.Parent.Top + control.Parent.Parent.Parent.Height-childHeight-86;
                    control2.Left = control.Parent.Parent.Parent.Left + control.Parent.Parent.Parent.Width - childWidth-10;
                    control2.Show();
                   // LibVlcAPI.SetParent(control2.Handle, control.Parent.Handle);
                }
                control.Focus();// 获得焦点，否则也得不到按键
                LibVlcAPI.SetParent(control.Handle, control.Parent.Handle);  
          }       
        }       
    }

    internal static class LibVlcAPI
    {
        internal struct PointerToArrayOfPointerHelper
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public IntPtr[] pointers;
        }

        public static IntPtr libvlc_new(string[] arguments)
        {
            PointerToArrayOfPointerHelper argv = new PointerToArrayOfPointerHelper();
            argv.pointers = new IntPtr[11];

            for (int i = 0; i < arguments.Length; i++)
            {
                argv.pointers[i] = Marshal.StringToHGlobalAnsi(arguments[i]);
            }

            IntPtr argvPtr = IntPtr.Zero;
            try
            {
                int size = Marshal.SizeOf(typeof(PointerToArrayOfPointerHelper));
                argvPtr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(argv, argvPtr, false);

                return libvlc_new(arguments.Length, argvPtr);
            }
            finally
            {
                for (int i = 0; i < arguments.Length + 1; i++)
                {
                    if (argv.pointers[i] != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(argv.pointers[i]);
                    }
                }
                if (argvPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(argvPtr);
                }
            }
        }

        public static IntPtr libvlc_media_new_path(IntPtr libvlc_instance, string path)
        {
            IntPtr pMrl = IntPtr.Zero;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(path);
                pMrl = Marshal.AllocHGlobal(bytes.Length + 1);
                Marshal.Copy(bytes, 0, pMrl, bytes.Length);
                Marshal.WriteByte(pMrl, bytes.Length, 0);
                return libvlc_media_new_path(libvlc_instance, pMrl);
            }
            finally
            {
                if (pMrl != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pMrl);
                }
            }
        }

        public static IntPtr libvlc_media_new_location(IntPtr libvlc_instance, string path)
        {
            IntPtr pMrl = IntPtr.Zero;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(path);
                pMrl = Marshal.AllocHGlobal(bytes.Length + 1);
                Marshal.Copy(bytes, 0, pMrl, bytes.Length);
                Marshal.WriteByte(pMrl, bytes.Length, 0);
                return libvlc_media_new_location(libvlc_instance, pMrl);
            }
            finally
            {
                if (pMrl != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pMrl);
                }
            }
        }

        //*** ----------------------------------------------------------------------------------------
        //    以下是libvlc.dll导出函数
        //***-----------------------------------------------------------------------------------------

        // 创建一个libvlc实例，它是引用计数的
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        private static extern IntPtr libvlc_new(int argc, IntPtr argv);

        // 释放libvlc实例
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_release(IntPtr libvlc_instance);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern String libvlc_get_version();

        // 从视频来源(例如Url)构建一个libvlc_meida
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        private static extern IntPtr libvlc_media_new_location(IntPtr libvlc_instance, IntPtr path);

        // 从本地文件路径构建一个libvlc_media
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        private static extern IntPtr libvlc_media_new_path(IntPtr libvlc_instance, IntPtr path);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_media_release(IntPtr libvlc_media_inst);

        // 创建libvlc_media_player(播放核心)
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern IntPtr libvlc_media_player_new(IntPtr libvlc_instance);

        // 将视频(libvlc_media)绑定到播放器上
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_media_player_set_media(IntPtr libvlc_media_player, IntPtr libvlc_media);

        // 设置图像输出的窗口
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_media_player_set_hwnd(IntPtr libvlc_mediaplayer, Int32 drawable);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_media_player_play(IntPtr libvlc_mediaplayer);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_media_player_pause(IntPtr libvlc_mediaplayer);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_media_player_stop(IntPtr libvlc_mediaplayer);

        // 
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_media_parse_async(IntPtr libvlc_media);

        // 解析视频资源的媒体信息(如时长等)
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_media_parse(IntPtr libvlc_media);

        // 返回视频的时长(必须先调用libvlc_media_parse之后，该函数才会生效)
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern Int64 libvlc_media_get_duration(IntPtr libvlc_media);

        // 当前播放的时间
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern Int64 libvlc_media_player_get_time(IntPtr libvlc_mediaplayer);

        // 设置播放位置(拖动)
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_media_player_set_time(IntPtr libvlc_mediaplayer, Int64 time);

        // 设置播放位置(拖动)
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_media_player_set_position(IntPtr libvlc_mediaplayer, float f_pos);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_media_player_release(IntPtr libvlc_mediaplayer);

        // 获取和设置音量
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern int libvlc_audio_get_volume(IntPtr libvlc_media_player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_audio_set_volume(IntPtr libvlc_media_player, int volume);

        // 设置全屏(非顶层窗体不能操作。这种情况下，启用全屏模式之前嵌入窗口必须被重设为父窗体，禁用全屏时重新设置父级其恢复到正常的父母。)
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_set_fullscreen(IntPtr libvlc_media_player, int isFullScreen);

        //设置和获取音量。
        [DllImport("winmm.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern long waveOutSetVolume(UInt32 deviceID, UInt32 Volume);

        [DllImport("winmm.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern long waveOutGetVolume(UInt32 deviceID, out UInt32 Volume);

        //全屏。
        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        //显示和隐藏任务栏
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]      
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        public const Int32 SW_SHOW = 5; public const Int32 SW_HIDE = 0;

         [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
          public static extern Int32 SystemParametersInfo(Int32 uAction, Int32 uParam, ref Rectangle lpvParam, Int32 fuWinIni);
          public const Int32 SPIF_UPDATEINIFILE = 0x1;
          public const Int32 SPI_SETWORKAREA = 47;
          public const Int32 SPI_GETWORKAREA = 48;

    }
}
