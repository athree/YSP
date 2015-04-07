using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using IMserver.Models;
using IMserver;
using System.Web.Security;

namespace WebApplication1.Account
{
    public partial class ManagePassword : System.Web.UI.Page
    {
        protected string SuccessMessage
        {
            get;
            private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // 确定要呈现的节

                changePasswordHolder.Visible = true;


                // 呈现成功消息
                var message = Request.QueryString["m"];
                if (message != null)
                {
                    // 从操作中去除查询字符串
                    Form.Action = ResolveUrl("~/Account/Manage");
                }
            }
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            bool changePasswordSucceeded=false;
            if (IsValid)
            {
                
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(CurrentPassword.Text, NewPassword.Text);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }
            }
            if (changePasswordSucceeded)
            {
                
                Response.Redirect("~/Account/Manage?m=ChangePwdSuccess");
            }
            else
            {
                ErrorMsg.Visible = true;
            }
        }



    }
}