using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMserver.CommonFuncs;
using IMserver;

namespace WebApplication1.App_Start
{
    public class CommStart
    {
        public CommStart()
        {
            try
            {
                //加载数据字典到内存，这里在调用XML前，暂时维持使用
                //define中的若干全局量是否只定义，统一组织，这里调用
                MyDictionary initialize = new MyDictionary();
                //初始化IMserver类，开启相关线程工作
                IMServerUDP current = new IMServerUDP();
                //如果工作线程随访问服务器开启，离开服务器关闭，或者保留心跳，那么这里另作网页离开事件处理

                //每次初始化系统的时候将数据库中的终端信息调入内存，在内存新入条目的时候也要更新数据库
                StrToEP ste = new StrToEP();
            }
            catch(Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }
    }
}