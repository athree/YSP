using Owin;

namespace WebApplication1
{
    public partial class Startup {

        // 有关配置身份验证的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301883
        //public void ConfigureAuth(IAppBuilder app)
        //{
            //// 配置数据库上下文、用户管理器和登录管理器，以便为每个请求使用单个实例
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            //// 使应用程序可以使用 Cookie 来存储已登录用户的信息
            //// 使用 Cookie 临时存储有关某个用户使用第三方登录提供程序进行登录的信息
            //// 配置登录 Cookie
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account/Login"),
            //    Provider = new CookieAuthenticationProvider
            //    {
            //        OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
            //            validateInterval: TimeSpan.FromMinutes(30),
            //            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
            //    }
            //});
           
        //}
    }
}
