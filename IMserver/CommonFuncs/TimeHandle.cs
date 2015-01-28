using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMserver.CommonFuncs
{
    public class TimeHandle
    {
        /// <summary>
        /// 这个方法既可以被   线程2   调用，也可以被    线程0（主线程）   调用
        /// </summary>
        /// <returns></returns>
        public static DateTime getcurrenttime()
        {
            DateTime currenttime = new DateTime();
            currenttime = DateTime.Now;
            return currenttime;
        }
    }
}
