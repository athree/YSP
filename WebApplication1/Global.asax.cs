using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Logic;
using WebApplication1.App_Start;
using System.Timers;
using System.Diagnostics;
using SysLog;
using System.Web.Security;

namespace WebApplication1
{
    public class Global : HttpApplication
    {
        private static Timer timer = new System.Timers.Timer();
        private static int[] mInterval = {1,1};  //mInterval[0]存储离下次备份还剩几个月，mInterval[1]存储实际备份频率
        FileLog bakFile = new FileLog(@"d:\data\bakSet.txt"); 
        

        public static int MInterval
        {
            get { return Global.mInterval[1]; }
            set { Global.mInterval[1] = value; setInterval(); }
        }

        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //注册通信相关的初始化项目，准备工作
            CommStart.CommConfig();

         
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
            //第一次启动时，文件不存在，设置备份间隔为1月。文件存在则设置为文件中设置的月份。         
            if (bakFile.Read() == null)
            {
                bakFile.Write("数据备份间隔月数:1");
                bakFile.Dispose();
            }           
            setInterval();
            timer.Elapsed += new ElapsedEventHandler(DataBak);
            timer.Start();                          
            

        }

        /// <summary>
        /// 数据备份，通过控制台命令行实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataBak(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(DateTime.Now.Day==1 && --mInterval[1]==0)
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


            mInterval[0] = mInterval[1];
            
            }
            //读取文件中存储的备份间隔，重新设置定时器           
            setInterval();

        }

        /// <summary>
        /// 设置备份时间间隔
        /// </summary>
        public static void setInterval()
        {
            FileLog bakFile = new FileLog(@"d:\data\bakSet.txt");
            string str = bakFile.Read().Substring(9, 1);
            mInterval[1] = Int32.Parse(str);
     
            //间隔1月触发
            DateTime currentT = DateTime.Now;
            DateTime nextT = currentT.AddMonths(1);
            DateTime bakT = new DateTime(nextT.Year, nextT.Month, 1, 0, 0, 0);
            double sub = (bakT - currentT).TotalMilliseconds;
            timer.Interval = sub>Int32.MaxValue?Int32.MaxValue:sub;
        }




        public void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //获取当前请求中保存有用户身份票据的Cookie  
            HttpCookie ticketCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            //如该Cookie不为空，如存在身份票据，解析票据信息，创建用户标识，获取用户角色  
            if (ticketCookie != null)
            {
                //获取用户票据  
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(ticketCookie.Value);

                string[] roles = ticket.UserData.Split(',');

                //创建用户标识  
                FormsIdentity identity = new FormsIdentity(ticket);

                //创建用户身份主体信息  
                System.Security.Principal.GenericPrincipal user = new System.Security.Principal.GenericPrincipal(identity, roles);

                //把由用户标识，角色信息组成的户身份主体信息保存在User属性中  
                HttpContext.Current.User = user;
            }
        }    
    
    
    }

}