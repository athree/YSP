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
        protected MongoHelper<RunningState> _rs = new MongoHelper<RunningState>();
        protected RunningState myRs;
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
            InitCtrls();
        }
        public void InitCtrls()
        {
            Expression<Func<RunningState, bool>> ex = p => p.DevID == devId;
            myRs = _rs.FindOneBy(ex);
            if (myRs == null)
            {
                Response.Write("<script>alert('我需要传输该设备信息！')</script>");
                return;
            }
            else
            {
                LB_89.Text = myRs.Temprature_Out.ToString();
                LB_88.Text = myRs.Temprature_In.ToString();
                LB_145.Text = myRs.GasPressure.ToString();
                LB_87.Text = myRs.OilTemprature.ToString();
                LB_64.Text = myRs.SePuZhuT.ToString();
                LB_62.Text = myRs.SensorRoomT.ToString();
                LB_63.Text = myRs.LengJingT.ToString();

                LB_166.Text = myRs.H2.ToString();
                LB_167.Text = myRs.CO.ToString();
                LB_169.Text = myRs.CO2.ToString();
                LB_168.Text = myRs.CH4.ToString();
                LB_170.Text = myRs.C2H2.ToString();
                LB_171.Text = myRs.C2H4.ToString();
                LB_172.Text = myRs.C2H6.ToString();
                LB_176.Text = myRs.TotHyd.ToString();
                LB_rs.Text = myRs.TotGas.ToString();
                LB_175.Text = myRs.Mst.ToString();
                LB_174.Text = myRs.T.ToString();
                LB_173.Text = myRs.AW.ToString();
            }
           
        }

       
        protected void BT_Data_Click(object sender, EventArgs e)
        {
            BT_Data.Text= "hello";
            ushort[] require = { 88, 87, 89, 86, 63, 61, 62, 165, 168, 169, 171, 173, 166, 167, 170, 175, 174, 172 };
            //初步测试期间不加心跳，故未能初始化Define.id_ip_port字典，这里添加以下，实际byte-ipendpoint的映射是在心跳处理中添加
            Define.id_ip_port.Add(0x01, new IPEndPoint(IPAddress.Parse("219.244.93.127"), 8888));
            //MyDictionary.ID_IP[0x01] = "219.244.93.127";
            //MyDictionary.ID_PORT[0x01] = 9999;
            //发送摘要缓冲
            PrepareData.Compare compare = new PrepareData.Compare();
            compare.srcID = 0x00;
            compare.destID = 0x01;
            //读操作单元的配置参数
            compare.msgType = (byte)MSGEncoding.MsgType.ReadUnit;
            compare.msgSubType = (byte)MSGEncoding.ReadUint.ReadData;
            compare.msgVer = MSGEncoding.msgVer;
            compare.msgDir = (byte)MSGEncoding.MsgDir.Request;
            //由于一个触发可能发送多包，所以编码在packet中组织，哈希入表也在packet中组织
            //触发组包
            byte temp = PrepareData.AddRequire(compare, require);
            //缓冲三秒
            while (!HandleData.hello.readone)
            {
                Thread.Sleep(100);
            }
                //为了跳出循环，这里不再复位标志位
            #region 赋值界面
            //修改为从数据库读取
            Dictionary<ushort, object> lady = (Dictionary<ushort , object>)HandleData.hello.result;
            for (int i = 0; i < lady.Count; i++)
            {
                ushort key = lady.ElementAt(i).Key;
                object value = lady.ElementAt(i).Value;
                switch (key)
                {
                    case 62:
                        {
                            LB_62.Text = ((float)value).ToString();
                            break;
                        }
                    case 63:
                        {
                            LB_63.Text = ((float)value).ToString();
                            break;
                        }
                    case 64:
                        {
                            LB_64.Text = ((float)value).ToString();
                            break;
                        }
                    case 87:
                        {
                            LB_87.Text = ((float)value).ToString();
                            break;
                        }
                    case 88:
                        {
                            LB_88.Text = ((float)value).ToString();
                            break;
                        }
                    case 89:
                        {
                            LB_89.Text = ((float)value).ToString();
                            break;
                        }
                    case 145:
                        {
                            LB_145.Text = ((float)value).ToString();
                            break;
                        }
                    case 166:
                        {
                            LB_166.Text = ((float)value).ToString();
                            break;
                        }
                    case 167:
                        {
                            LB_167.Text = ((float)value).ToString();
                            break;
                        }
                    case 168:
                        {
                            LB_168.Text = ((float)value).ToString();
                            break;
                        }
                    case 169:
                        {
                            LB_169.Text = ((float)value).ToString();
                            break;
                        }
                    case 170:
                        {
                            LB_170.Text = ((float)value).ToString();
                            break;
                        }
                    case 171:
                        {
                            LB_171.Text = ((float)value).ToString();
                            break;
                        }
                    case 172:
                        {
                            LB_172.Text = ((float)value).ToString();
                            break;
                        }
                    case 173:
                        {
                            LB_173.Text = ((float)value).ToString();
                            break;
                        }
                    case 174:
                        {
                            LB_174.Text = ((float)value).ToString();
                            break;
                        }
                    case 175:
                        {
                            LB_175.Text = ((float)value).ToString();
                            break;
                        }
                    case 176:
                        {
                            LB_176.Text = ((float)value).ToString();
                            break;
                        }
                    default://总可燃气体
                        {
                            LB_rs.Text = "why";
                            break;
                        }
                }
            }
                #endregion
        }
    }
}