using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using IMserver.Models.SimlDefine;
using IMserver.Data_Warehousing;

namespace WebApplication1.DataProcess
{
    /// <summary>
    /// ysp 的摘要说明
    /// </summary>
    public class ysp : IHttpHandler
    {
        HttpRequest gRequest = null;
        HttpContext gContext = null;
        HttpResponse gResponse = null;
        string cmd = string.Empty;
        string result = string.Empty;
        string pageUrl = string.Empty;
        String devId = string.Empty;
        String gas = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            gContext = context;
            gRequest = gContext.Request;
            gResponse = gContext.Response;
            cmd = gRequest["cmd"];
            devId = gRequest["devId"];
            gas = gRequest["gas"]==null?string.Empty:gRequest["gas"];
            MethodInfo method = typeof(ysp).GetMethod(cmd);
            if(method!=null)
            {
                object[] args = new object[] { result};
                method.Invoke(this, args);
                result = (string)args[0];
            }
            gResponse.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }



        public void getCalPara(out string result) 
        {

            Dictionary<ushort,object> cpDic=new IMserver.GetData().getCalP();
            Dictionary<string, ushort> myDic = stretchDic(cpDic);

            string strResult = Newtonsoft.Json.JsonConvert.SerializeObject(myDic);            
            result = strResult;
            try
            {
                AddSIML.Warehousing(cpDic, Byte.Parse(devId));
            }
            catch (Exception e)
            {

            }
 
        }


        public void getK(out string result)
        {
            Dictionary<ushort, object> kDic = new IMserver.GetData().getK(gas);
            string strResult = Newtonsoft.Json.JsonConvert.SerializeObject(kDic);
            result = strResult;
            try
            {
                AddSIML.Warehousing(kDic, Byte.Parse(devId));
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// 字典转换，将Dictionary<ushort,object>转换为Dictionary<string,ushort>，
        /// 将字典中value为ushort[]的键值对进行拆分，拆分后的key为OldKey_Index
        /// </summary>
        /// <param name="dic">原字典Dictionary<ushort,object></param>
        /// <returns>转换后的字典Dictionary<string,ushort></returns>
        public Dictionary<string, ushort> stretchDic(Dictionary<ushort, object> dic)
        {
            Dictionary<string, ushort> myDic = new Dictionary<string, ushort>();
            string key;
            ushort[] value;
            for(int i=0;i<dic.Count();i++)
            {
                var ele = dic.ElementAt(i);
                key = ele.Key.ToString();
                value =(ushort[])ele.Value;
                for(int j=0;j<value.Length;j++)
                {
                    key = key +"_"+ j;
                    myDic.Add(key, value[j]);
                }
            }
            return myDic;
        }
        
    }
}