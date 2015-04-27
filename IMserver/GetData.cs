using IMserver.Data_Warehousing;
using IMserver.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace IMserver
{
    public class GetData
    {
        protected PrepareData.Compare compare;
        public GetData(){
            compare = new PrepareData.Compare();            
            //初步测试期间不加心跳，故未能初始化Define.id_ip_port字典，这里添加以下，实际byte-ipendpoint的映射是在心跳处理中添加
            //Define.id_ip_port.Add(0x01, new IPEndPoint(IPAddress.Parse("223.104.11.107"), 9997));
            //MyDictionary.ID_IP[0x01] = "219.244.93.127";
            //MyDictionary.ID_PORT[0x01] = 9999;
            //发送摘要缓冲            
            compare.srcID = 0x00;
            compare.destID = 0x01;
            //读操作单元的配置参数
            compare.msgType = (byte)MSGEncoding.MsgType.ReadUnit;
            compare.msgSubType = (byte)MSGEncoding.ReadUint.ReadData;
            compare.msgVer = MSGEncoding.msgVer;
            compare.msgDir = (byte)MSGEncoding.MsgDir.Request;
        }
        /// <summary>
        /// 从下位机获取RunningStae运行状态数据
        /// </summary>
        /// <returns></returns>
        public Dictionary<ushort, object> GetRS()
        {
            ushort[] require = { 88, 87, 89, 86, 63, 61, 62, 165, 168, 169, 171, 173, 166, 167, 170, 175, 174, 172 };
            //由于一个触发可能发送多包，所以编码在packet中组织，哈希入表也在packet中组织
            //触发组包
            byte temp = PrepareData.AddRequire(compare, require);
            //缓冲三秒
            while (!HandleData.hello.readone)
            {
                Thread.Sleep(100);
            }
            //为了跳出循环，这里不再复位标志位

            //修改为从数据库读取
            Dictionary<ushort, object> lady = (Dictionary<ushort, object>)HandleData.hello.result;
            return lady;

        }
        /// <summary>
        /// 从下位机获取历史时间段的RunningStae运行状态数据
        /// </summary>
        /// <param name="begintime">用户选取的开始时间</param>
        /// <param name="endtime">用户选取的结束时间</param>
        /// <returns></returns>
        public bool GetRS(string begintime, string endtime) 
        {
            bool Flag = false;
            ////这里写如何提取历史运行状态信息的，成功则修改Flag为true
            return Flag;
        }
        /// <summary>
        /// 从下位机获取AlarmState报警状态信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<ushort, object> GetAS() 
        {
            ushort[] require = {286,287,288,135,145,64,62,63,136,154,11,13,14,15,16,17,18,19,7,8};
            //由于一个触发可能发送多包，所以编码在packet中组织，哈希入表也在packet中组织
            //触发组包
            byte temp = PrepareData.AddRequire(compare, require);
            //缓冲三秒
            while (!HandleData.hello.readone)
            {
                Thread.Sleep(100);
            }
            //为了跳出循环，这里不再复位标志位

            //修改为从数据库读取
            Dictionary<ushort, object> lady = (Dictionary<ushort, object>)HandleData.hello.result;
            return lady;
           
        }



        /// <summary>
        /// 获取气体一级二级报警注意值等设置
        /// </summary>
        /// <param name="devid">设备id</param>
        /// <returns>报警状态类</returns>
        public Dictionary<ushort, object> GetAttention()
        {                  
            ushort[] require = {};
            for (int i = 291; i < 362; i++)
            {
                ushort[] concat = { (ushort)i };
                require.Concat(concat);
            }
            //由于一个触发可能发送多包，所以编码在packet中组织，哈希入表也在packet中组织
            //触发组包
            byte temp = PrepareData.AddRequire(compare, require);
            //缓冲三秒
            while (!HandleData.hello.readone)
            {
                Thread.Sleep(100);
            }
            //为了跳出循环，这里不再复位标志位

            //修改为从数据库读取
            Dictionary<ushort, object> lady = (Dictionary<ushort, object>)HandleData.hello.result;
            return lady;
        }




        /// <summary>
        /// 从下位机获取系统配置信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<ushort, object> GetSys()
        {
            ushort[] require = {150,151,152,149,182,184,180,181,183};
            //由于一个触发可能发送多包，所以编码在packet中组织，哈希入表也在packet中组织
            //触发组包
            byte temp = PrepareData.AddRequire(compare, require);
            //缓冲三秒
            while (!HandleData.hello.readone)
            {
                Thread.Sleep(100);
            }
            //为了跳出循环，这里不再复位标志位

            //修改为从数据库读取
            Dictionary<ushort, object> lady = (Dictionary<ushort, object>)HandleData.hello.result;
            return lady;

        }
        /// <summary>
        /// 获取控制参数CtrlParam
        /// </summary>
        /// <param name="tqfs">脱气方式0：真空 1：膜 2：顶空</param>
        /// <returns></returns>
        public Dictionary<ushort, object> GetCP(int tqfs)
        {
            ushort[] require = { 78,79,80,81,82,83,84,66,67,68,69,70,71,72,73,74,75,76,77,97,98,99,100,101,102,103,104,131,107,108,109,110,123,124,111,112,113,114,120,121,122,127,128,125,126,129,130,117,118,115,116};
            switch(tqfs)
            {
                case 0:
                    {
                        ushort[] tq = { 4, 5, 6, 7, 8, 9 };
                        require.Concat(tq);
                        break;
                    }                    
                case 1:
                    {
                        ushort[] tq = { 36,37,39,41,38,40,42};
                        require.Concat(tq);
                        break;
                    }
                case 2:
                    {
                        ushort[] tq = { 51,52,53,54,55,56 };
                        require.Concat(tq);
                        break;
                    }
            }

            //由于一个触发可能发送多包，所以编码在packet中组织，哈希入表也在packet中组织
            //触发组包
            byte temp = PrepareData.AddRequire(compare, require);
            //缓冲三秒
            while (!HandleData.hello.readone)
            {
                Thread.Sleep(100);
            }
            //为了跳出循环，这里不再复位标志位

            //修改为从数据库读取
            Dictionary<ushort, object> lady = (Dictionary<ushort, object>)HandleData.hello.result;
            return lady;

        }
        /// <summary>
        /// 获取状态控制信息StateCtrl
        /// </summary>
        /// <param name="tqfs">脱气方式0：真空 1：膜 2：顶空</param>
        /// <returns></returns>
        public Dictionary<ushort, object> GetSC(int tqfs)
        {
            ushort[] require = {59,60,62,63,64,87,88,89,96,92,93,94,95,133,134,135,136,145,138,139,140,141,142,143,144 };
            switch (tqfs)
            {
                case 0:
                    {
                        ushort[] tq = { 13,14,18,19,12,15,16,17,11,21,22,23,24,25,26,27,28,33,29,30,31,32 };
                        require.Concat(tq);
                        break;
                    }
                case 1:
                    {                       
                        break;
                    }
                case 2:
                    {
                        ushort[] tq = { 45,47,48,49 };
                        require.Concat(tq);
                        break;
                    }
            }

            //由于一个触发可能发送多包，所以编码在packet中组织，哈希入表也在packet中组织
            //触发组包
            byte temp = PrepareData.AddRequire(compare, require);
            //缓冲三秒
            while (!HandleData.hello.readone)
            {
                Thread.Sleep(100);
            }
            //为了跳出循环，这里不再复位标志位

            //修改为从数据库读取
            Dictionary<ushort, object> lady = (Dictionary<ushort, object>)HandleData.hello.result;
            return lady;

        }

    }
}