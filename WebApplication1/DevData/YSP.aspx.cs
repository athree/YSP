using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMserver.DBservice;
using IMserver.Models;
using IMserver.Models.SimlDefine;
using IMserver.CommonFuncs;
using IMserver;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace WebApplication1.DevData
{
    public partial class YSP : System.Web.UI.Page
    {
        protected MongoHelper<SIML> _siml = new MongoHelper<SIML>();
        protected SIML mySiml;
        public string devId,devSite,devName,devType;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            devId = HttpUtility.UrlDecode(Request.QueryString["DevID"]);
            devSite = HttpUtility.UrlDecode(Request.QueryString["DevSite"]);
            devType = HttpUtility.UrlDecode(Request.QueryString["DevType"]);
            devName = HttpUtility.UrlDecode(Request.QueryString["DevName"]);
            Session["DevID"] = devId;
            Session["DevSite"] = devSite;
            Session["DevType"] = devType;
            Session["DevName"] = devName;
            Session["SelectedData"] = null;
            Session["SelectedAbs"] = null;
            Session["SelectedRel"] = null;
            SIML_GetItem();
        }
        public void SIML_GetItem()
        {
            Expression<Func<SIML, bool>> ex = p => p.DevID == devId;
            mySiml = _siml.FindOneBy(ex);
            if (mySiml == null)
            {
                Response.Write("<script>alert('未找到相应设备信息！')</script>");
                return;
            }
            Session["DataID"] = mySiml.DataID.ToString();
            
        }

        public OutSideStateCtrl OutSideSC_GetItem()
        {
            return mySiml.SC.OutSideSC;
        }
        public ContentData Content_GetItem()
        {
            return mySiml.Content;
        }

        protected void BT_Data_Click(object sender, EventArgs e)
        {
            MyDictionary initialize = new MyDictionary();
            BT_Data.Text = "hello";
            IMServerUDP current = new IMServerUDP();
            ushort[] require = { 88, 87, 89, 86, 63, 61, 62, 165, 168, 169, 171, 173, 166, 167, 170, 175, 174, 172 };
            //初步测试期间不加心跳，故未能初始化Define.id_ip_port字典，这里添加以下，实际byte-ipendpoint的映射是在心跳处理中添加
            Define.id_ip_port.Add(0x01, new IPEndPoint(IPAddress.Parse("219.244.93.127"), 9999));
            //MyDictionary.ID_IP[0x01] = "219.244.93.127";
            //MyDictionary.ID_PORT[0x01] = 9999;
            //发送摘要缓冲
            PrepareData.Compare compare = new PrepareData.Compare();
            compare.srcID = 0x00;
            compare.destID = 0x01;
            //读操作单元的配置参数
            compare.msgType = (byte)MSGEncoding.MsgType.ReadUnit;
            compare.msgSubType = (byte)MSGEncoding.ReadUint.GetDevStatus;
            compare.msgVer = MSGEncoding.msgVer;
            compare.msgDir = (byte)MSGEncoding.MsgDir.Request;
            //由于一个触发可能发送多包，所以编码在packet中组织，哈希入表也在packet中组织
            //触发组包
            byte temp = PrepareData.AddRequire(compare, require);
            //缓冲三秒
            while (!HandleData.hello.readone)
            { }
                //为了跳出循环，这里不再复位标志位
            #region 赋值界面
            Dictionary<ushort, object> lady = (Dictionary<ushort , object>)HandleData.hello.result;
            for (int i = 0; i < lady.Count; i++)
            {
                ushort key = lady.ElementAt(i).Key;
                object value = lady.ElementAt(i).Value;
            }
                #endregion
        }
    }
}