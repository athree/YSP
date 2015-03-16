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
            string userName = UserName.Text;
            string password = Password.Text;
            MembershipUser newUser=null;
            try
            {
                newUser = Membership.CreateUser(userName, password);
            }
            catch(Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }     
            
               
            if (newUser!=null)
            {
                ErrorMessage.Text = "恭喜， "+userName+"  用户已成功创建！";
            }
          
        }
    }
}