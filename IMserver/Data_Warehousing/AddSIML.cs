using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMserver.Models;
using IMserver.Models.SimlDefine;

namespace IMserver.Data_Warehousing
{
    public class AddSIML
    {
        /// <summary>
        /// !!!异常
        /// </summary>
        /// <param name="middata"></param>
        /// <param name="devid"></param>
        /// <returns></returns>
        public static bool Warehousing(Dictionary<ushort, object> middata, byte devid)
        {
            SIML sl = new SIML();
            sl.SC = new StateCtrol();
            sl.AnalyInfo = new AnlyInformation();
            //这个类是否为下端读取上来的操作单元或者是否全不是
            try
            {
                return Warehousing(sl);
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }
        /// <summary>
        /// !!!异常
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool Warehousing(SIML s)
        {
            try
            {
                return false;
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }
    }
}