using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.SessionState;
namespace WebApplication1.DevDebug
{
    /// <summary>
    /// GetGcVal 的摘要说明
    /// </summary>
    public class GetGcVal : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            object a;
            try
            {
                a=context.Session["gcVal"];
                               
            }
            catch (Exception ex)
            {
                a = ex.Message;
            }
            string outJson=JsonConvert.SerializeObject(a); 
            context.Response.Write(outJson);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}