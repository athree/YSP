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

            //为了避免不足一个类的数据字典来填充，这里使用switch-case语法，兼容直接的类成员罗列赋值
            foreach(KeyValuePair<ushort , object> kvp in middata)
            {
                switch (kvp.Key)
                {
                    case 62:
                        {
                            rs.SensorRoomT = (float)kvp.Value;
                            break;
                        }
                    case 63:
                        {
                            rs.LengJingT = (float)kvp.Value;
                            break;
                        }
                    case 64:
                        {
                            rs.SePuZhuT = (float)kvp.Value;
                            break;
                        }
                    case 87:
                        {
                            rs.OilTemprature = (float)kvp.Value;
                            break;
                        }
                    case 88:
                        {
                            rs.Temprature_In = (float)kvp.Value;
                            break;
                        }
                    case 89:
                        {
                            rs.Temprature_Out = (float)kvp.Value;
                            break;
                        }
                    case 166:
                        {
                            rs.H2 = (float)kvp.Value;
                            rs.TotGas += rs.H2;
                            break;
                        }
                    case 167:
                        {
                            rs.CO = (float)kvp.Value;
                            rs.TotGas += rs.CO;
                            break;
                        }
                    case 168:
                        {
                            rs.CH4 = (float)kvp.Value;
                            rs.TotGas += rs.CH4;
                            break;
                        }
                    case 169:
                        {
                            rs.CO2 = (float)kvp.Value;
                            break;
                        }
                    case 170:
                        {
                            rs.C2H2 = (float)kvp.Value;
                            rs.TotGas += rs.C2H2;
                            break;
                        }
                    case 171:
                        {
                            rs.C2H4 = (float)kvp.Value;
                            rs.TotGas += rs.C2H4;
                            break;
                        }
                    case 172:
                        {
                            rs.C2H6 = (float)kvp.Value;
                            rs.TotGas += rs.C2H6;
                            break;
                        }
                    case 173:
                        {
                            rs.AW = (float)kvp.Value;
                            break;
                        }
                    case 174:
                        {
                            rs.T = (float)kvp.Value;
                            break;
                        }
                    case 175:
                        {
                            rs.Mst = (float)kvp.Value;
                            break;
                        }
                    case 176:
                        {
                            rs.TotHyd = (float)kvp.Value;
                            break;
                        }
                    case 177:
                        {
                            rs.GasPressure = (float)kvp.Value;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            //rs.TotGas在响应读到可燃气体的时候累加
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