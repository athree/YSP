using System;


namespace WebApplication1
{
    public partial class Map : System.Web.UI.Page
    {
       
    
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //接收DevType和DevName

            //string devId = Request.QueryString["DevID"];
            //if(devId==null)
            //{
            //    DataFormView.DataSource = _siml.FindAll().AsQueryable();
            //    DataFormView.DataBind();
            //    UpdatePanel1.Update();
     
            //}
            //else
            //{
            //    DataFormView.DataSource = _siml.FindAll().AsQueryable().Where(p => p.DevID == devId);
            //    DataFormView.DataBind();
            //    UpdatePanel1.Update();
            // }    
           
        }

        // id 参数应与控件上设置的 DataKeyNames 值匹配
        // 或用值提供程序特性装饰，例如 [QueryString]int id

        /// <summary>
        /// 测试用，提取SIML内容不加限制，第二个弹窗中总显示第一条数据。。。。
        /// </summary>
        /// <returns></returns>
     
        /// <summary>
        /// 获取DevID与查询字符相等的SIML对象
        /// </summary>
        /// <returns></returns>
        //public IQueryable<SIML> SIML_GetItem()
        //{
        //    string devId = Request.QueryString["DevID"];
        //    Expression<Func<SIML, bool>> ex = p => p.DevID == devId;
        //    //mySiml = _siml.FindBy(ex);
        //    return _siml.FindAll().AsQueryable().Where(ex);
        //}
       
       

      
    }
}