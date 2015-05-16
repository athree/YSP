using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace IMserver
{
    public class SetData
    {
        protected PrepareData.Compare compare;
        protected byte devId;
        public SetData(){
            compare = new PrepareData.Compare();            
            //初步测试期间不加心跳，故未能初始化Define.id_ip_port字典，这里添加以下，实际byte-ipendpoint的映射是在心跳处理中添加
            //Define.id_ip_port.Add(0x01, new IPEndPoint(IPAddress.Parse("223.104.11.107"), 9997));
            //MyDictionary.ID_IP[0x01] = "219.244.93.127";
            //MyDictionary.ID_PORT[0x01] = 9999;
            //发送摘要缓冲            
            compare.srcID = 0x00;
            compare.destID = 0x01;
            //读操作单元的配置参数
            compare.msgType = (byte)MSGEncoding.MsgType.WriteUnit;
            compare.msgSubType = (byte)MSGEncoding.WriteUint.WriteData;
            compare.msgVer = MSGEncoding.msgVer;
            compare.msgDir = (byte)MSGEncoding.MsgDir.Request;
        }


        public bool setSys(Dictionary<ushort,object> data) { 
            
            byte temp = PrepareData.AddRequire(compare, data);
           
            while (!HandleData.hello.readone)
            {
                Thread.Sleep(100);
            }
            //为了跳出循环，这里不再复位标志位

            //修改为从数据库读取
            bool flag = (bool)HandleData.hello.result;

            return flag;
        }
    }
}