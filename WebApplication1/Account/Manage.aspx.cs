using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using IMserver.Models;
using IMserver;
using System.Web.Security;

namespace WebApplication1.Account
{
    public partial class Manage : System.Web.UI.Page
    {
        protected string SuccessMessage
        {
            get;
            private set;
        }


        public int LoginsCount { get; set; }

        protected void Page_Load()
        {


            //var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            if (!IsPostBack)
            {

                ChangePassword.Visible = true;


                // 呈现成功消息
                var message = Request.QueryString["m"];
                if (message != null)
                {
                    // 从操作中去除查询字符串
                    Form.Action = ResolveUrl("~/Account/Manage");

                    SuccessMessage =
                        message == "ChangePwdSuccess" ? "已更改你的密码。"
                        : message == "SetPwdSuccess" ? "已设置你的密码。"
                        : message == "RemoveLoginSuccess" ? "该帐户已删除。"
                        : message == "AddPhoneNumberSuccess" ? "已添加电话号码"
                        : message == "RemovePhoneNumberSuccess" ? "已删除电话号码"
                        : String.Empty;
                    successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }
            }
            //if (HttpContext.Current.User.IsInRole("Admin"))
            //{
            //    ManageUsers.Visible = true;
            //}

            //private void AddErrors(IdentityResult result)
            //{
            //    foreach (var error in result.Errors)
            //    {
            //        ModelState.AddModelError("", error);
            //    }
            //}
        }

    }
}