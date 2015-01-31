using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMserver.Models;
using IMserver.DBservice;
using IMserver.CommonFuncs;

namespace IMserver.Data_Warehousing
{
    public class AddRunningState
    {
        /// <summary>
        /// 通信结果入库
        /// </summary>
        /// <param name="middata">调用此方法入库默认传入的数据字典为请求的完成无误响应</param>
        /// <returns></returns>
        public static bool Warehousing(Dictionary<ushort , object> middata , byte devid)
        {
            RunningState rs = new RunningState();
            rs.DevID = devid.ToString();
            rs.ReadDate = DateTime.Now;

            rs.AW = (float)middata[173];
            rs.C2H2 = (float)middata[170];
            rs.C2H4 = (float)middata[171];
            rs.C2H6 = (float)middata[172];
            rs.CH4 = (float)middata[168];
            rs.CO = (float)middata[167];
            rs.CO2 = (float)middata[169];
            rs.GasPressure = (float)middata[177];
            rs.H2 = (float)middata[166];
            rs.LengJingT = (float)middata[63];
            rs.Mst = (float)middata[174];
            rs.OilTemprature = (float)middata[87];
            rs.SensorRoomT = (float)middata[62];
            rs.SePuZhuT = (float)middata[64];
            rs.T = (float)middata[174];
            rs.Temprature_In = (float)middata[88];
            rs.Temprature_Out = (float)middata[89];
            rs.TotGas = rs.C2H2 + rs.C2H4 + rs.C2H6 + rs.CH4 + rs.CO + rs.H2;　//总可燃气体
            rs.TotHyd = (float)middata[176];
            return Warehousing(rs);
        }

        /// <summary>
        /// 前台操作入库或者直接类映射入库
        /// </summary>
        /// <param name="directdata">与数据库集合对应的类</param>
        /// <returns></returns>
        public static bool Warehousing(RunningState directdata)
        {
            MongoHelper<RunningState> rs = new MongoHelper<RunningState>();
            return rs.Insert(directdata);
        }
    }
}