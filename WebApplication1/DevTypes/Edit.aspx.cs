using System;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using IMserver.DBservice;
using System.Linq.Expressions;
using IMserver.Models;
using MongoDB.Bson;
namespace WebApplication1.DevTypes
{
    public partial class Edit : System.Web.UI.Page
    {
        protected MongoHelper<DevType> _DevType = new MongoHelper<DevType>();
        protected MongoHelper<DevSite> _devSite=new MongoHelper<DevSite>();
        protected MongoHelper<DevType> _devType = new MongoHelper<DevType>();
        protected Expression<Func<DevType, bool>> ex;
        public DevType item;

        protected void Page_Load(object sender, EventArgs e)
        {
          
            
        }

        //protected void Page_LoadComplete(object sender, EventArgs e)
        //{
        //   // (MyFormView.FindControl("MyDevType") as DropDownList).SelectedValue = item.Type;
        //    //(MyFormView.FindControl("MyCompName") as DropDownList).SelectedValue = item.CompName;
        //}

        // This is the Update methd to update the selected DevType_view item
        // USAGE: <asp:FormView UpdateMethod="UpdateItem">
        public void UpdateItem(string  TypeId)
        {

            ex = p => p.TypeId==new ObjectId(TypeId);
            var item = _DevType.FindOneBy(ex);
            if (item == null)
            {
                // The item wasn't found
                //ModelState.AddModelError("", String.Format("Item with id {0} was not found", TypeId));
                ModelState.AddModelError("","找不到该设备");
                   
                return;
            }
            
            TryUpdateModel(item);
  

            if (ModelState.IsValid)
            {
                _DevType.Update(item);
            }
            Response.Redirect("../Default");
            
        }

        // This is the Select method to selects a single DevType_view item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public IMserver.Models.DevType GetItem([FriendlyUrlSegmentsAttribute(0)]string TypeId)
        {
            if (TypeId == null)
            {
                return null;
            }
            ex = p => p.TypeId == new ObjectId(TypeId);
            item = _DevType.FindOneBy(ex);
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
