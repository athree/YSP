using MongoDB.Bson;
using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using IMserver.DBservice;
using IMserver.Models;

namespace WebApplication1
{
    /// <summary>
    /// 对~/Scripts/Map.js传来的ajax请求进行处理，
    /// 获取特定地点的设备列表
    /// </summary>
    public class getDev : IHttpHandler
    {
        protected MongoHelper<DevInfo> _devInfo = new MongoHelper<DevInfo>();
        protected IList<DevInfo> devInfoes;
        public void ProcessRequest(HttpContext context)
        {
          
            context.Response.ContentType = "text/plain";

            //接收参数locateID
            string CompName = HttpUtility.UrlDecode(context.Request.QueryString["CompName"]);
              
            Expression<Func<DevInfo, bool>> ex = p => p.CompName == CompName;

            //按照类型不同来排序,降序
            devInfoes = _devInfo.FindBy(ex).OrderByDescending(p => p.Type).ToList();
        
            
            JsonOutputMode opt =JsonOutputMode.Strict;
            JsonWriterSettings settings = new JsonWriterSettings();
            settings.OutputMode = opt;
           
            string outJson = devInfoes.ToJson(settings);
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