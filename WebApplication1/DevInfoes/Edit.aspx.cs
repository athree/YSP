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
using System.Linq.Expressions;
using IMserver.Models;
namespace WebApplication1.DevInfoes
{
    public partial class Edit : System.Web.UI.Page
    {
        protected MongoHelper<DevInfo> _devInfo = new MongoHelper<DevInfo>();
        protected MongoHelper<DevSite> _devSite=new MongoHelper<DevSite>();
        protected MongoHelper<DevType> _devType = new MongoHelper<DevType>();
        protected Expression<Func<DevInfo, bool>> ex;
        public DevInfo item;

        protected void Page_Load(object sender, EventArgs e)
        {
            (MyFormView.FindControl("MyDevType") as DropDownList).SelectedValue = ViewState["Type"].ToString();
            (MyFormView.FindControl("MyCompName") as DropDownList).SelectedValue = ViewState["Comp"].ToString();
            
        }       

        // This is the Update methd to update the selected DevInfo_view item
        // USAGE: <asp:FormView UpdateMethod="UpdateItem">
        public void UpdateItem(string  DevID,string CompName)
        {

            ex = p => p.DevID == DevID;
            var item = _devInfo.FindOneBy(ex);  
            if (item == null)
            {
                // The item wasn't found
                //ModelState.AddModelError("", String.Format("Item with id {0} was not found", DevID));
                ModelState.AddModelError("","找不到该设备");
                   
                return;
            }
            item.Type = (MyFormView.FindControl("MyDevType") as DropDownList).SelectedValue;
            item.CompName = (MyFormView.FindControl("MyCompName") as DropDownList).SelectedValue;
            TryUpdateModel(item);
  

            if (ModelState.IsValid)
            {
                _devInfo.Update(item);
            }
            Response.Redirect("../Default");
            
        }

        // This is the Select method to selects a single DevInfo_view item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public IMserver.Models.DevInfo GetItem([FriendlyUrlSegmentsAttribute(0)]string DevID)
        {
            if (DevID == null)
            {
                return null;
            }
            ex = p => p.DevID == DevID;
            item = _devInfo.FindOneBy(ex);
            ViewState["Type"] = item.Type;
            ViewState["Comp"] = item.CompName;
            return item;
            
        }

        public IQueryable<DevSite> GetDevSite()
        {
            //MongoHelper<DevSite>_devSite = new MongoHelper<DevSite>();
            return _devSite.FindAll().AsQueryable();
        }

        public IQueryable<DevType> GetDevType()
        {
            _devType = new MongoHelper<DevType>();

            return _devType.FindAll().AsQueryable();

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
