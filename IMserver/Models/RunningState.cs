using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace IMserver.Models
{
    public class rsIdGenerator : IIGenerator{}

    //运行状态界面  

    [BsonDiscriminator("runningstate")]
    public class RunningState
    {
        [BsonId(IdGenerator = typeof(rsIdGenerator))]
        public int ID{get;set;}
        public string DevID { get; set; }
        public DateTime ReadDate { get; set; }

        //第一个界面
        public float OilTemprature { get; set; }		//油温	
        public float Temprature_In{ get; set; }		//柜内温度
        public float Temprature_Out { get; set; }    //柜外温度
        public float GasPressure { get; set; }  		//载气压力检测实际值
        public float SePuZhuT { get; set; }            //色谱柱温度
        public float SensorRoomT { get; set; }       //检测室温度
        public float LengJingT { get; set; }           //萃取温度，又叫冷井温度
        public float H2 { get; set; }             //氢气浓度
        public float CO { get; set; }
        public float CH4 { get; set; }
        public float CO2 { get; set; }
        public float C2H2 { get; set; }
        public float C2H4 { get; set; }
        public float C2H6 { get; set; }
        public float AW { get; set; }            //微水活性
        public float T { get; set; }               //微水温度
        public float Mst { get; set; }               //微水浓度
        public float TotHyd { get; set; }            //总烃
        public float TotGas { get; set; }            //总可燃气体
    }
}