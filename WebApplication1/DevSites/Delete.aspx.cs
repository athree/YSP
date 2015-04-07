using System;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using IMserver.DBservice;
using MongoDB.Bson;
using IMserver.Models;

namespace WebApplication1.DevSites
{
    public partial class Delete : System.Web.UI.Page
    {
        protected MongoHelper<DevSite> _devSite = new MongoHelper<DevSite>();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This is the Delete methd to delete the selected DevSite item
        // USAGE: <asp:FormView DeleteMethod="DeleteItem">
        public void DeleteItem(string LocateID)
        {
            var a = _devSite.Delete(new ObjectId(LocateID));
            Response.Redirect("../Default");
        }

        // This is the Select methd to selects a single DevSite item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public IMserver.Models.DevSite GetItem([FriendlyUrlSegmentsAttribute(0)]string LocateID)
        {
            if (LocateID == null)
            {
                return null;
            }
           
             return _devSite.FindById(new ObjectId(LocateID));
            
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

