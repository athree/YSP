using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Logic;
using WebApplication1.App_Start;

namespace WebApplication1
{
    public class Global : HttpApplication
    {
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
        }
    }
}