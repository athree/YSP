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

namespace WebApplication1.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            MembershipCreateStatus createStatus;
            Membership.CreateUser(UserName.Text, Password.Text, null, null, null, true, null, out createStatus);

            if (createStatus == MembershipCreateStatus.Success)
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