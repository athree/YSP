using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using IMserver.DBservice;
using IMserver.Models;

namespace WebApplication1.DevSites
{
    public partial class Insert : System.Web.UI.Page
    {
        protected MongoHelper<DevSite> _devSite = new MongoHelper<DevSite>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // This is the Insert method to insert the entered DevSite item
        // USAGE: <asp:FormView InsertMethod="InsertItem">
        public void InsertItem()
        {
            var item = new IMserver.Models.DevSite();

            TryUpdateModel(item);

            if (ModelState.IsValid)
            {
                // Save changes
                _devSite.Insert(item);

                Response.Redirect("Default");
                
            }
        }

        protected void ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("Default");
            }
        }
    }
}
