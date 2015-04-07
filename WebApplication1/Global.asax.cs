using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Logic;
using WebApplication1.App_Start;
using System.Timers;
using System.Diagnostics;
using SysLog;

namespace WebApplication1
{
    public class Global : HttpApplication
    {
        private static Timer timer = new System.Timers.Timer();
        private static int mInterval = 1;

        public static int MInterval
        {
            get { return Global.mInterval; }
            set { Global.mInterval = value; setInterval(); }
        }

        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            try
            {
                //注册通信相关的初始化项目，准备工作
                CommStart cs = new CommStart();
            }
            catch(Exception ep)
            {
                LogException(ep);
            }
         
            //创建管理员角色和用户
            RoleActions roleActions = new RoleActions();
            roleActions.createRole();
            //设备类型初始化
            DevTypeIni devTypeIni = new DevTypeIni();

            //设置每月1号0点0分0秒备份，若启动当天为1号，则立即备份          
            if (DateTime.Now.Day == 1)
            {
                DataBak(timer, null);
            }
            setInterval();
            timer.Elapsed += new ElapsedEventHandler(DataBak);
            //timer.Start();

        }

        /// <summary>
        /// 数据备份，通过控制台命令行实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataBak(object sender, System.Timers.ElapsedEventArgs e)
        {
            string path = @"cd D:\mongo\bin\";
            string cmd = @"mongodump -d test -o d:\data\backup\" + DateTime.Now.ToString("yyyy-MM-dd");
            string clear = "mongo --eval \"db.person.drop()\"";

            Process process = new System.Diagnostics.Process();
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe");
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            process.StartInfo = startInfo;
            process.Start();

            process.StandardInput.WriteLine("d:");
            process.StandardInput.WriteLine(path);
            process.StandardInput.WriteLine(cmd);
            process.StandardInput.WriteLine(clear);
            process.StandardInput.WriteLine("exit");
            string netMessage = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            setInterval();

        }

        /// <summary>
        /// 设置备份时间间隔
        /// </summary>
        public static void setInterval()
        {
            DateTime currentT = DateTime.Now;
            DateTime nextT = currentT.AddMonths(mInterval);
            DateTime bakT = new DateTime(nextT.Year, nextT.Month, 1, 0, 0, 0);
            timer.Interval = (bakT - currentT).TotalMilliseconds;
        }

        private void LogException(Exception ex)
        {
            using (ILog log = new FileLog())
            {
                log.Write("Exception from Class: "+this.ToString());
                log.Write("Exception:" + ex.Message);
                log.Write(ex.StackTrace);
                log.Write("时间：" + DateTime.Now.ToString());
                log.Write("----------------------------------------------------------");
            }
        }
    }
}