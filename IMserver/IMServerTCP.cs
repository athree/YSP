using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using IMserver.CommonFuncs;

namespace IMserver
{
    public class IMServerTCP
    {

        #region  测试使用的结构体 , C#中默认的结构体中成员为私有的
        //调用C++动态链接库的结构体使用规范，这里layoutkind为内存中的排序，charset为编码规范，pack为对齐方式此处即按一字节对齐
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct hello
        {
            public int what;
            public double where;
            /*
            //字符串，SizeConst为字符串的最大长度
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string str;
            //int数组，SizeConst表示数组的个数，在转换成byte数组前必须先初始化数组
            //再使用初始化的数组长度必须和SizeConst一致，例test = new int[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public int[] test;
             */
        }
        #endregion

        #region 测试使用的方法
        public byte[] result;
        public void test()
        {
            hello testtemp;
            testtemp.what = 1;
            testtemp.where = 1.234;
            result = Encoding.ASCII.GetBytes(testtemp.ToString());
            testtemp.what = 2;
        }
        #endregion
        static Socket connfd = null;
        static Socket listenfd = null;

        public struct TCPSynchField
        {
            public Socket lisenfd;
            public Socket connfd;
            public EndPoint remotepoint;               //本次通信连接的远端终结点
            public int recvnum;                        //本次接收的字节数
            public bool udpsendflag;           //有TCP数据要发送标识
            public bool udprecvflag;           //有TCP数据要接受标志
            public bool donerecvflag;          //接收完成标志
            [MarshalAs(UnmanagedType.ByValArray , SizeConst = 256)]
            public byte[] sendbuffer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] recvbuffer;
        }

        /// <summary>
        /// 开启TCP
        /// </summary>
        /// <param name="history">
        /// 参数是否为引用类型，这里测试引用效果
        /// 这里是否默认就是引用类型
        /// C#中大多都是引用类型传递，只有个别的基本类型为值类型
        /// </param>
        public static void StartTCP()
        {            
            listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress temp = IPAddress.Parse("219.244.93.60");
            IPEndPoint ipep = new IPEndPoint(temp, 12345);
            listenfd.Bind(ipep);
            listenfd.Listen(20);
            ThreadStart listenthread = new ThreadStart(WatchConnection);
            Thread watchconn = new Thread(listenthread);
            watchconn.IsBackground = true;
            watchconn.Start();
        }

        /// <summary>
        /// 当有连接发生的时候接受连接 ，这个线程会阻塞在accept这个方法，当有链接发生的时候，跳出accept方法继续执行
        /// 并新开出一个    进程2      来发送消息
        /// </summary>
        public static void WatchConnection()
        {
            for (; ; )
            {   //有一个连接就开一个线程出来，即单个线程处理一个套接字，在开线程的同时就把套接字传递给新开线程
                connfd = listenfd.Accept();             //会一直阻塞在这里，知道有链接发生，才会继续向下运行
                IPEndPoint temp = (IPEndPoint)connfd.RemoteEndPoint;

                hello test;
                test.what = 1;
                test.where = 123.243;
                //这里是有一个连接就会开出一个  线程
                ParameterizedThreadStart commthread = new ParameterizedThreadStart(IMServerTCP.RecvMsg);
                Thread comm = new Thread(commthread);
                comm.Start(connfd);
            }
        }

        /// <summary>
        /// 在主线程中被调用的发送方法   进程0
        /// </summary>
        public static void SendMsg()
        {
            byte[] buffer = Encoding.UTF8.GetBytes("textdata to send...");
            connfd.Send(buffer);
        }

        /// <summary>
        /// 在  线程2   中执行的接受数据的方法
        /// </summary>
        /// <param name="parasocket"></param>
        public static void RecvMsg(object parasocket)
        {
            Socket commsocket = parasocket as Socket;
            for (; ; )
            {
                byte[] buffer = new byte[1024];
                int length = commsocket.Receive(buffer);
                string mess = Encoding.UTF8.GetString(buffer, 0, length);
            }
        }

        /// <summary>
        /// 数据包解析函数
        /// </summary>
        public void ExplainMessage()
        {
            byte []hello = new byte[1024];
            hello = Encoding.ASCII.GetBytes("hello");
            
            //MessageBox.Show(hello.Length.ToString());
        }
    }
}
