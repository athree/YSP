using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IMserver.Models.SimlDefine;

namespace IMserver.Models
{
    [BsonDiscriminator("config")]
    public class Config
    {
        [ScaffoldColumn(false)]
        public ObjectId _id;

        [Key, Required, Display(Name = "设备号")]
        public string DevID { get; set; }

        //环境及外围设置
        public OutSideSetting OutSideSet { get; set; }

        //分析计算参数
        public AnalysisParameter AnalyPara { get; set; }

        //采样控制
        public SampleSetting SampSet { get; set; }

        //采样开始控制
        public SampleStartting SampStart { get; set; }

        //系统设置
        public SystemSetting SysSet { get; set; }

        //脱气设置
        public TuoQiSetting TQSet { get; set; }

        //检测辅助设置
        public JCFZSetting JCFZSet { get; set; }

        //报警及分析配置
        public AlarmAll Alarm { get; set; }
    }
}