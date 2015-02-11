using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using IMserver.Models;
using IMserver;
using System.Web.Security;
using WebApplication1.Logic;

namespace WebApplication1.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            RoleActions roleA=new RoleActions();
            bool createStatus = roleA.createUser(UserName.Text, Password.Text, null);          
            if (createStatus)
            {
                FormsAuthentication.SetAuthCookie(UserName.Text, false /* createPersistentCookie */);
                Response.Redirect(Request.QueryString["ReturnUrl"], true);
            }
            else
            {
                ErrorMessage.Text = createStatus.ToString();
            }
        }
    }
}