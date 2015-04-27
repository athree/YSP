using IMserver.Models.SimlDefine;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMserver.Models
{

    //报警界面


    [BsonDiscriminator("alarmstate")]
    public class AlarmState
    {
       
        public string DevID { get; set; }

        public DateTime Time_Stamp { get; set; }

        public AlarmAll aa { get; set; }
       
        public DateTime NextSampleTime { get; set; }   //下次采样时间
        public float GasPressure { get; set; }      //载气压力检测实际值
        public float VacuPres { get; set; }//（脱气机）真空度压力检测值
        public float OilPres { get; set; }//油压检测值
        public char YouBeiLevel { get; set; }//油杯液位状态
        public char QiBeiLevel { get; set; }//气杯液位状态
        public char QiGangForw { get; set; }//气缸进到位
        public char QiGangBackw { get; set; }//气缸退到位
        public char YouGangForw { get; set; }//油缸进到位
        public char YouGangBackw { get; set; }//油缸退到位
        public char TuoQiTimes { get; set; }//脱气次数查询/设置
        public char ChangeTimes { get; set; }//置换次数查询/设置
        public float SensorRoomT { get; set; }    //传感器室温度实际采样值
        public float LengJingT { get; set; }    //冷井温度实际采样值
        public float SePuZhuT { get; set; }    //色谱柱温度实际采样值
        public ushort SampleInterval { get; set; }  //采样间隔
        public DateTime PLCTime { get; set; }  //系统时间，下位机
    }
}