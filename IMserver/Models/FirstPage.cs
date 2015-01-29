using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace IMserver.Models
{
    public class FirstPage
    {
        //第一个界面
        public float OilTemprature;		//油温	
        public float Temprature_In;		//柜内温度
        public float Temprature_Out;    //柜外温度
        public float QiBengPres;  		//载气发生器气泵压力检测实际值
        public float SePu_T;            //色谱柱温度
        public float Jianceshi_T;       //检测室温度
        public float Cuiqu_T;           //萃取温度
        public float H2ppm;
        public float COppm;
        public float CH4ppm;
        public float CO2ppm;
        public float C2H2ppm;
        public float C2H4ppm;
        public float C2H6ppm;
        public float WtrAct;             //微水活性
        public float Tmp;                //微水温度
        public float Mst;                //微水浓度
        public float TotHyd;             //总烃
        public float ToGas;              //总可燃气体

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