using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using IMserver.DBservice;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using IMserver.Models;

namespace WebApplication1
{
    /// <summary>
    /// addMyMarkers 的摘要说明
    /// </summary>
    public class addMyMarkers : IHttpHandler
    {
        public IList<DevSite> myDevSite;
        protected MongoHelper<DevSite> _devSite = new MongoHelper<DevSite>();
      



        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            JsonOutputMode opt =JsonOutputMode.Strict;
            var settings = new JsonWriterSettings();
            settings.OutputMode = opt;
            myDevSite = _devSite.FindAll().ToList();
            string mySite = myDevSite.ToJson(settings);
         
          
              
            if (mySite.Count()>0)           
            {
                context.Response.Write(mySite);

            }
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