using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMserver
{
    public class PreLinkMsg
    {

        /// <summary>
        /// 组织发往DTU，让DTU连接GPRS并发送心跳到主站，传入的是DTU的号码，返回信息字符串
        /// </summary>
        /// <param name="PhoneNumber"> 终端DTU的号码，发送文本信息使用</param>
        /// <returns> 返回信息字符串</returns>
        public static string LinkMsg(string PhoneNumber)
        {
            string downmsg = "短信通知终端发起向中心的UDP连接";
            return downmsg;
        }
    }
}
