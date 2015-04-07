using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;
using IMserver.DBservice;
using IMserver.Models;
using IMserver.CommonFuncs;

namespace IMserver.CommonFuncs
{
    public class StrToEP
    {
        /// <summary>
        /// 远端终结点类型的字符串形式转换为终结点类
        /// ！！！异常
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public IPEndPoint trans(string temp)
        {
            try
            {
                IPAddress IPadr = IPAddress.Parse(temp.Split(':')[0]);
                IPEndPoint EndPoint = new IPEndPoint(IPadr, int.Parse(temp.Split(':')[1]));
                return EndPoint;
            }
            catch(Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }

        /// <summary>
        /// 将设备号到IP与端口的映射提到内存
        /// ！！！异常
        /// </summary>
        public StrToEP()
        {
            MongoHelper<DevID_EP> mhde = new MongoHelper<DevID_EP>();
            try
            {
                IQueryable<DevID_EP> temp = mhde.FindAll();
                foreach (var item in temp)
                {
                    Define.id_ip_port.Add((byte)Int16.Parse(item.DevID), trans(item.endpoint));
                }
            }
            catch (Exception ex)
            {
                //调用它的上一层接收
                throw new Exception(ex.Message);
            }
        }
    }
}