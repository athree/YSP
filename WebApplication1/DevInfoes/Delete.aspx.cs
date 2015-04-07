using IMserver.DBservice;
using IMserver.Models;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using System;
using System.Linq.Expressions;
using System.Web.UI.WebControls;

namespace WebApplication1.DevInfoes
{
    public partial class Delete : System.Web.UI.Page
    {
        protected MongoHelper<DevInfo> _devInfo = new MongoHelper<DevInfo>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This is the Delete methd to delete the selected DevInfo_view item
        // USAGE: <asp:FormView DeleteMethod="DeleteItem">
        public void DeleteItem(string DevID)
        {
            Expression<Func<DevInfo, bool>> ex = p=>p.DevID == DevID;
            _devInfo.Delete(ex);
            Response.Redirect("../Default");
        }

        // This is the Select methd to selects a single DevInfo_view item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public IMserver.Models.DevInfo GetItem([FriendlyUrlSegmentsAttribute(0)]string DevID)
        {
            if (DevID == "")
            {
                return null;
            }
            Expression<Func<DevInfo, bool>> ex = p => p.DevID == DevID;
            return _devInfo.FindOneBy(ex);
            
        }

        protected void ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("../Default");
            }
        }
    }
}

