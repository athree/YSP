using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace IMserver.Models
{
    [BsonDiscriminator("runningstate")]
    public class RunningState
    {

        public int ID;
        public string DevID { get; set; }

        public DateTime ReadDate { get; set; }
        //第一个界面
        public float OilTemprature { get; set; }		//油温	
        public float Temprature_In{ get; set; }		//柜内温度

        public float Temprature_Out { get; set; }    //柜外温度

        public float GasPressure { get; set; }  		//载气压力检测实际值
        public float SePuZhuT { get; set; }            //色谱柱温度
        public float SensorRoomT { get; set; }       //检测室温度

        public float LengJingT;           //萃取温度，又叫冷井温度

  
        public float H2;
        public float CO;
        public float CH4;
        public float CO2;
        public float C2H2;
        public float C2H4;
        public float C2H6;
        public float AW;             //微水活性
        public float T;                //微水温度
        public float Mst;                //微水浓度
        public float TotHyd;             //总烃
        public float TotGas;              //总可燃气体

        /// <summary>
        /// 将解析到的键值对转换为相应的类实例，用hashtable接收
        /// </summary>
        /// <param name="final">最终使用类</param>
        /// <param name="rawdata">原始解析到的键值对</param>
        public void K_V_PairToClass(Hashtable final, Dictionary<ushort, object> rawdata)
        {

        }
    }
}