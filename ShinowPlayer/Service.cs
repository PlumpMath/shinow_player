using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace ShinowPlayer
{
    public static class Service
    {       
        public static FormPlayer FormPlayer { get; set; }

        public static FormChild FormChild { get; set; }

        public static string PluginPath{
            get
            {
                return System.Environment.CurrentDirectory + "\\plugins\\";
            }
        }

        public static string[] VideoList { get; set; }

        public static int GetVideoLength(string file)
        {
            int res = 0;
            using (System.Diagnostics.Process pro = new System.Diagnostics.Process())
            {
                pro.StartInfo.CreateNoWindow = true;
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.ErrorDialog = false;
                pro.StartInfo.RedirectStandardError = true;

                pro.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "ffmpeg.exe";
                pro.StartInfo.Arguments = " -i " + file;

                try
                {
                    pro.Start();
                    System.IO.StreamReader errorreader = pro.StandardError;
                    pro.WaitForExit(1000);

                    string result = errorreader.ReadToEnd();
                    if (!string.IsNullOrEmpty(result))
                    {
                        result = result.Substring(result.IndexOf("Duration: ") + ("Duration: ").Length, ("00:00:00").Length);
                        if (result.Length == 8)
                        {
                            res = int.Parse(result.Substring(0, 2)) * 60 * 60 + int.Parse(result.Substring(3, 2)) * 60 + int.Parse(result.Substring(6, 2));
                        }
                    }
                }
                catch
                {
                }
            }
            return res;
        }
    }
}
