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
        public static bool Warehousing(Dictionary<ushort, object> middata, byte devid)
        {
            SIML sl = new SIML();
            sl.SC = new StateCtrol();
            sl.AnalyInfo = new AnlyInformation();
            //这个类是否为下端读取上来的操作单元或者是否全不是
            return false;
        }
        public static bool Warehousing(SIML s)
        {
            return false;
        }
    }
}