using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Account
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {         
            AllUsersBind();
           
        }

        protected void AllUsersBind()
        {
            MembershipUserCollection allUsers = Membership.GetAllUsers();
            allUsers.Remove("admin");
            AllUsers.DataSource = allUsers;
            AllUsers.DataBind();
        }       



        protected void AllUsers_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            string userName =AllUsers.DataKeys[e.ItemIndex].Value.ToString();
            bool deleteSuccessed = false;
            try
            {
                deleteSuccessed = Membership.DeleteUser(userName);
            }
            catch(Exception ex)
            {               
                MsgText.Text = "用户删除失败！";
                return;
            }            
            if(deleteSuccessed)
            {
                AllUsers.DeleteItem(e.ItemIndex);
                MsgText.Text = userName + "用户删除成功";
            }
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath);
        }
    }
}