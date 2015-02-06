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
        public IPEndPoint trans(string temp)
        {
            IPAddress IPadr = IPAddress.Parse(temp.Split(':')[0]);
            IPEndPoint EndPoint = new IPEndPoint(IPadr, int.Parse(temp.Split(':')[1]));
            return EndPoint;
        }

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