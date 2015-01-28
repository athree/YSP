
namespace IMserver.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using IMserver.Models.SimlDefine;

    [BsonDiscriminator("siml")]
    public class SIML
    {
        [BsonId]
        public ObjectId DataID { get; set; }  //使该条数据唯一的编号

        [Display(Name = "设备号")]

        public string DevID;
        public DateTime Time_Stamp { get; set; }  //时间戳

        #region 数据信息
        //气体和微水含量
        public ContentData Content;

        //采样原始数据
        public RawData Raw;

        //气体其它数据
        public SGasOtherData OtherData;

        #endregion


       ////配置信息
       // public Config CFG { get; set; }
       
     


        //状态/控制信息
       
        public StateCtrol SC{ get; set; }

     

      
        // 诊断分析

        public AnlyInformation AnalyInfo { get; set; }



        //告警信息
        public enum AlarmType
        {
            Content = 0,            //含量超标      
            Absolute,               //绝对产期速率超标
            Relative                //相对产期速率超标
        }

        public AlarmMsgAll[] AlarmMsg { get; set; }


        //出峰顺序
        public enum PeekOrder
        {
            H2 = 0,
            CO,
            CH4,
            C2H2,
            C2H4,
            C2H6,
            CO2
        }


        public static int GetGasFixParameters(GasFixK[] mk, float area)
        {
            int sel = 0;

            for (int i = 0; i < mk.Length; i++)
            {
                if (area >= mk[i].areaMin && area <= mk[i].areaMax)
                {
                    sel = i;
                    break;
                }
            }
            return sel;
        }

      
    }

   
   
}



