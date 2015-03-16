using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using IMserver.Models;
using IMserver;
using System.Web.Security;
using System.Security.Principal;
using WebApplication1.Logic;

namespace WebApplication1.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterHyperLink.NavigateUrl = "Register";
            
       }

        //protected void LogIn(object sender, EventArgs e)
        //{
        //    if (IsValid)
        //    {
        //        // 验证用户密码
        //        var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //        var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

        //        // 这不会计入到为执行帐户锁定而统计的登录失败次数中
        //        // 若要在多次输入错误密码的情况下触发锁定，请更改为 shouldLockout: true
        //        var result = signinManager.PasswordSignIn(UserName.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

        //        switch (result)
        //        {
        //            case SignInStatus.Success:
                      //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        //                break;
        //            case SignInStatus.LockedOut:
        //                Response.Redirect("/Account/Lockout");
        //                break;
        //            case SignInStatus.RequiresVerification:
        //                Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}", 
        //                                                Request.QueryString["ReturnUrl"],
        //                                                RememberMe.Checked),
        //                                  true);
        //                break;
        //            case SignInStatus.Failure:
        //            default:
        //                FailureText.Text = "无效的登录尝试";
        //                ErrorMessage.Visible = true;
        //                break;
        //        }
        //    }
        //}
        public void LogIn(object sender, EventArgs e)
        {
            if (ModelState.IsValid)
            {
                string username = UserName.Text;
                string password = Password.Text;
                bool isPersistent = RememberMe.Checked;
                if (Membership.ValidateUser(username,password))
                {

                    RoleActions roleAct = new RoleActions();
                    string roles = roleAct.GetRolesForUser(username);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30), isPersistent, roles, FormsAuthentication.FormsCookiePath);

                    // Encrypt the ticket.
                    string encTicket = FormsAuthentication.Encrypt(ticket);



                    // Create the cookie.
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                    //////string authenticationType = windowsIdentity.AuthenticationType;
                    //////GenericIdentity genericIdentity = new GenericIdentity(username, authenticationType);

                    ////// Construct a GenericPrincipal object based on the generic identity
                    ////// and custom roles for the user.
                    ////GenericPrincipal genericPrincipal = new GenericPrincipal(fIdentity, roles);
                    ////HttpContext.Current.User = genericPrincipal;


                    
                   
                    //FormsAuthentication.SetAuthCookie(username+"|"+roles, isPersistent);
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(username, isPersistent));

                 
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    FailureText.Text = "无效的登录尝试,用户名或密码错误！";
                    ErrorMessage.Visible = true;
                }
            }
         
        }
    }
}