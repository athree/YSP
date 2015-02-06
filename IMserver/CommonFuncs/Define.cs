using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace IMserver.CommonFuncs
{

    public class Define
    {
        //每次图文件最大长度，这个考虑随网络状况动态变化（LoopBack）
        public static ushort TransFile_MAX = 256;
        //数据包计数，用以对照返回数据包，清除计时队列，在packetzation方法中对该量自加
        //先在packetzation中相应位置截断插入数据帧编号，待稳定后再修改相应的帧组装方法
        //public static byte packetnum = 0;  转移到IMServerUDP中定义为私有的成员，
        // 并作pakcetzation方法的返回值供外用


        public struct SendQueueItem
        {
            public EndPoint destpoint;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public byte[] senddata;
        }

        public struct RecvQueueItem
        {
            public EndPoint srcpoint;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public byte[] recvdata;
        }

        #region  最小操作单元对应的类，已经把对这些类的引用转移到model.simldefine中
        //气体填充参数A
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct GasFixPara_A
        {
            //Gas峰顶点可能位置
            public ushort PeakPoint;
            //Gas峰顶范围起点
            public ushort PeakLeft;
            //Gas峰顶范围结束点
            public ushort PeakRight;
            //Gas峰顶宽度
            public ushort PeakWidth;          
        }
        public static int GFP_A_LEN = Marshal.SizeOf(typeof(GasFixPara_A));

        //气体填充参数B
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct GasFixPara_B
        {
            //Gas左梯度YMin
            public ushort Left_YMin;
            //Gas左梯度XMax
            public ushort Left_XMax;
            //Gas右梯度YMin
            public ushort Right_YMin;
            //Gas右梯度XMax
            public ushort Right_XMax;
        }
        public static int GFP_B_LEN = Marshal.SizeOf(typeof(GasFixPara_B)); 

        //微水修正参数AW和T
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct H2OAnlyParam	
        {
            //微水AW修正系数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public float[] h2oAwFix;
            //微水T修正系数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public float[] h2oTFix;			
        }
        public static int HAP_LEN = Marshal.SizeOf(typeof(H2OAnlyParam)); 

        //微水修正参数AW
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct H2OAnlyParam_AW
        {
            //微水修正参数AW-A
            public float H2OAnlyParam_AW_A;
            //微水修正参数AW-K
            public float H2OAnlyParam_AW_K;
            //微水修正参数AW-B
            public float H2OAnlyParam_AW_B;
        }
        public static int HAP_AW_LEN = Marshal.SizeOf(typeof(H2OAnlyParam_AW));
        
        //微水修正参数T
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct H2OAnlyParam_T
        {
            //微水修正参数T-A
            public float H2OAnlyParam_T_A;
            //微水修正参数T-K
            public float H2OAnlyParam_T_K;
            //微水修正参数T-B
            public float H2OAnlyParam_T_B;
        }
        public static int HAP_T_LEN = Marshal.SizeOf(typeof(H2OAnlyParam_T));

        // 多级K值
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct GasFixParameters
        {
            //K值
            public float k;
            //柱修正系数
            public float mi;
            //脱气率修正系数
            public float ni;
            //该K值对应的最小面积
            public float areaMin;
            //该K值对应的最大面积
            public float areaMax;               
        }
        public static int GFP_LEN = Marshal.SizeOf(typeof(GasFixParameters));

        // 权当做色谱图剔除区间
        /// 气路从六组分切换到CO2的时候，应该从六组分采样气体中剔除CO2的采样区间，
        /// 如果start和end都为0的话，不剔除
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct EraseRange
        {
            public ushort start;
            public ushort end;
        }
        public static int ER_LEN = Marshal.SizeOf(typeof(EraseRange));

        //油品系数
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct EnvironmentSetting
        {
            //油品系数A（4bytes）
            public float oilFactorA;
            //油品系数B（4bytes）  
            public float oilFactorB;          
        }
        public static int ES_LEN = Marshal.SizeOf(typeof(EnvironmentSetting));
        #endregion

        //定时器的实现机制是否为跨线程，不知，这里暂且将都添加volatile修饰

        //信息发送队列
        public static volatile Queue<Define.SendQueueItem> send_queue = new Queue<Define.SendQueueItem>();
        //信息接收队列
        public static volatile Queue<Define.RecvQueueItem> recv_queue = new Queue<Define.RecvQueueItem>();

        //提交消息计时表
        //public static Hashtable existime = new Hashtable();

        public static volatile Dictionary<byte, int> existime = new Dictionary<byte, int>();
        //超时的未响应队列
        public static volatile Queue<Define.SendQueueItem> send_queue_timeout = new Queue<SendQueueItem>();
        //远端终结点的链表集合，方便使用select扫描（在RecvMsg中）
        public static volatile List<EndPoint> endpointlist = new List<EndPoint>();
        //编号和发送摘要缓存
        public static volatile Hashtable index_obj = new Hashtable();
        //编号和数据包附加描述信息
        public static volatile Hashtable index_com = new Hashtable();
        //用于记录终端发送心跳包的时间
        public static volatile Dictionary<byte, ushort> heartcheck = new Dictionary<byte, ushort>();
        //用于记录设备ID到IP和端口的查询字典
        public static volatile Dictionary<byte, IPEndPoint> id_ip_port = new Dictionary<byte, IPEndPoint>();
    }
}
