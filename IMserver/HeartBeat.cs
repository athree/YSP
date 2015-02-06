using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using IMserver.CommonFuncs;
using IMserver.DBservice;
using IMserver.Models;
using System.Linq.Expressions;
using SysLog;

namespace IMserver
{
    public class HeartBeat
    {
        public const ushort heartinterval = 30;      //默认的两个心跳包之间的间隔
        public const int Head_LEN = 7;              //帧内数据域长度
        public const string _FRAME_HEAD = "XYBUS";              //帧头
        public static byte[] BUS_FLAG = new byte[]        //数据帧头字节数组
                                         { 0x58, 0x59, 0x42, 0x55, 0x53, 0x00, 0x00 };
        //心跳包格式
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct Heart_Bus
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Head_LEN )]
            public byte[] head;                     //数据域
            public byte myid;
        }

        public static int Heart_Bus_Len = Marshal.SizeOf(typeof(Heart_Bus));  //包长=8
        public static int Default_Len = 64;

        /// <summary>
        /// 心跳检测，看和loopback组合
        /// </summary>
        public static void HeartCheck()
        {
            //每一秒钟检查一次数据字典清楚超时请求
            HeartTimer();

            for (; ; )
            {
                byte[] buffer = new byte[Default_Len];
                EndPoint remotecli = new IPEndPoint(IPAddress.Any, 0);
                int recv = IMServerUDP.shared.heartfd.ReceiveFrom(buffer, ref remotecli);
                if (Heart_Bus_Len == recv)
                {
                    try
                    {
                        byte[] temp = new byte[recv];
                        Array.Copy(buffer, 0, temp, 0, recv);
                        Heart_Bus hb = (Heart_Bus)ByteStruct.BytesToStruct(temp, typeof(Heart_Bus));
                        //是指定格式的心跳包
                        if (8 == temp.Length)
                        {
                            byte temp_id = hb.myid;


                            //已经存在的设备
                            if (Define.heartcheck.Keys.Contains(temp_id))
                            {
                                ushort nowtime = Define.heartcheck[temp_id];
                                Define.heartcheck[temp_id] = heartinterval;
                                //在这里更新可能断线后重连的终端的ID和IP与PORT
                                if (!Define.id_ip_port[temp_id].Equals((IPEndPoint)remotecli))
                                {
                                    Define.id_ip_port[temp_id] = (IPEndPoint)remotecli;
                                    //想要更新某条记录，需先要取出该条记录更改，再update
                                    Expression<Func<DevID_EP, bool>> ex = p => p.DevID == ((Int16)temp_id).ToString();
                                    //需要时再开数据库
                                    MongoHelper<DevID_EP> stp = new MongoHelper<DevID_EP>();
                                    DevID_EP diep = stp.FindOneBy(ex);
                                    diep.endpoint = remotecli.ToString();
                                    stp.Update(diep);
                                }
                            }
                            //该设备不存在，暂时可以认为是新增设备，先添加进心跳检测队列
                            else
                            {
                                Define.heartcheck.Add(temp_id, heartinterval);
                                //新增设备注册到设备号到IP和PORT的记录字典
                                Define.id_ip_port.Add(temp_id, (IPEndPoint)remotecli);
                                //需要时再开数据库
                                MongoHelper<DevID_EP> stp = new MongoHelper<DevID_EP>();
                                DevID_EP temp_de = new DevID_EP();
                                temp_de.DevID = temp_id.ToString();
                                temp_de.endpoint = remotecli.ToString();
                                stp.Insert(temp_de);
                            }
                            //接收到心跳包，原样返回,是在以上处理完成之后发送
                            IMServerUDP.shared.heartfd.SendTo(temp, remotecli);
                        }
                    }
                    catch (Exception ep)
                    {
                        LogException(ep);
                    }
                }
                else 
                {
                    Thread.Sleep(50);
                }
            }
        }

        /// <summary>
        /// 对心跳包进行减计时处理
        /// </summary>
        public static void HeartTimeHandle(object source, System.Timers.ElapsedEventArgs e)
        {
            for (int i = 0; i < Define.heartcheck.Count; i++)
            {
                ushort value = Define.heartcheck.ElementAt(i).Value;
                byte key = Define.heartcheck.ElementAt(i).Key;
                if (0 == value)
                {
                    //MessageBox.Show(key.ToString()+" 号设备已经下线！\r\n");
                    Define.heartcheck.Remove(key);
                }
                else
                {
                    Define.heartcheck[key]= (ushort)(value - 1);
                }
            }
        }

        /// <summary>
        /// 定时处理入口
        /// </summary>
        public static void HeartTimer()
        {
            TimerTick requiretimeoutcheck = new TimerTick(1000, HeartTimeHandle);
            //requiretimeoutcheck.StartTimer();
        }

        private static void LogException(Exception ex)
        {
            using (ILog log = new FileLog())
            {
                log.Write("Exception from Class: HeartBeat");
                log.Write("Exception:" + ex.Message);
                log.Write(ex.StackTrace);
                log.Write("时间：" + DateTime.Now.ToString());
                log.Write("----------------------------------------------------------");
            }
        }
    }
}
