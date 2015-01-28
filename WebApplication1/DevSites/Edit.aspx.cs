using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using IMserver.DBservice;
using MongoDB.Bson;
using IMserver.Models;

namespace WebApplication1.DevSites
{
    public partial class Edit : System.Web.UI.Page
    {
        protected MongoHelper<DevSite> _devSite = new MongoHelper<DevSite>();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This is the Update methd to update the selected DevSite item
        // USAGE: <asp:FormView UpdateMethod="UpdateItem">
        public void UpdateItem(string  LocateID)
        {
            DevSite item = _devSite.FindById(new ObjectId(LocateID));
            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", LocateID));
                return;
            }

            TryUpdateModel(item);

            if (ModelState.IsValid)
            {
                // Save changes here
                _devSite.Update(item);
                Response.Redirect("../Default");
            }
            }
       

        // This is the Select method to selects a single DevSite item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public DevSite GetItem([FriendlyUrlSegmentsAttribute(0)]string LocateID)
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
