using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Configuration;

namespace ShinowPlayer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //args = new[] {"2.mp4","3.mp4" };
           // args = new[] { "http://10.1.1.170/zhongjiezhe.mp4" , "http://172.0.16.80:8800/overview.mp4"};

            var vt = GetTheVideoType(ref args);
            if (vt != VideoType.Other)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new FormMain(args,vt));
            }

        }

        private static VideoType GetTheVideoType(ref string[] args)
        {
            var tv = VideoType.LocalVideo;
            var videosPath = ConfigurationManager.AppSettings["VideosPath"];
            var localVideosPath = new string[args.Length];
            for (var i = 0; i < args.Length; i++)
            {
                localVideosPath[i] = Path.Combine(videosPath, args[i]);
            }
            foreach (var localVideoPath in localVideosPath)
            {
                if (!File.Exists(localVideoPath))
                {
                    tv = VideoType.Url;
                    break;
                }
            }
            if (tv == VideoType.LocalVideo)
            {
                args = localVideosPath;
                return tv;
            }
            foreach (var urlItem in args)
            {
                if (!UrlIsExist(urlItem))
                {
                    tv = VideoType.Other;
                    break;
                }
            }
            return tv;
        }

        private static bool UrlIsExist(String url)
        {
            System.Uri u = null;
            try
            {
                u = new Uri(url);
            }
            catch { return false; }
            bool isExist = false;
            var r = System.Net.WebRequest.Create(u)
                                    as System.Net.HttpWebRequest;
            r.Method = "HEAD";
            try
            {
                var s = r.GetResponse() as System.Net.HttpWebResponse;
                if (s.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    isExist = true;
                }
            }
            catch (System.Net.WebException x)
            {
                try
                {
                    var httpWebResponse = x.Response as System.Net.HttpWebResponse;
                    if (httpWebResponse != null)
                        isExist = (httpWebResponse.StatusCode !=
                                   System.Net.HttpStatusCode.NotFound);
                }
                catch { isExist = (x.Status == System.Net.WebExceptionStatus.Success); }
            }
            return isExist;
        }
    }

    public enum VideoType
    {
        Url=1,
        LocalVideo=2,
        Other=3
    }
}
