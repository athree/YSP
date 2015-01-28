using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMserver.Models.SimlDefine
{
    public class StateCtrol
    {
        public ZhenKongStateCtrl ZhenKongSC; //真空状态/控制
        public DingKongStateCtrl DingKongSC; //顶空状态控制
        public JCFZStateCtrl JCFZSC;     //检测辅助状态控制
        public OutSideStateCtrl OutSideSC;           //环境外围状态控制
        public SampleStateCtrl SampSC;         //采样状态控制   
        public SystemStateCtrl SysSC;   //系统设置状态控制
    }
}