using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.Timers;
using IMserver.CommonFuncs;
using IMserver.SubFuncs;
using System.IO;


namespace IMserver
{
    /// <summary>
    /// 用来检验接受到的数据帧是否为所请求的内容
    /// 也可以在帧校验中进行检测
    /// </summary>
    public struct SendType
    {
        public byte srcID;                      //源设备号
        public byte destID;                     //目标设备号
        public byte msgType;                    //消息类型
        public byte msgSubType;                 //消息子类型 
    }

    public class UDPSynchField
    {
        public IPAddress hostaddress;               //服务器段即本地IPEndPoint
        public Socket listenfd;                     //处理本次通信的套接口
        public Socket heartfd;                      //处理心跳包的套接口（为心跳包专门开一个套接口，单独协议）
        public EndPoint remotepoint;                //本次通信连接的远端终结点
        //public int recvnum;                       //本次接收的字节数
        public bool udpsendflag = false;           //有UDP数据要发送标识
        public bool udprecvflag = false;           //有UDP数据要接受标志
        public bool donerecvflag = false;          //接收完成标志
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        //public byte[] recvbuffer;                  //接收缓冲区
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] sendbuffer;                  //发送缓冲区指针

        //public UDPSynchField()
        //{
        //    recvbuffer = new byte[4096];
        //}
    }

    public class IMServerUDP:TimerTick
    {
        //#region   测试使用的传递参数
        //public static TextBox history;
        //public static TextBox message;
        //#endregion
        //public static Socket listenfd;
        //public static EndPoint remotepoint;

        //public void helloworld()
        //{
        //    MessageBox.Show("this method is used just only by new a real example out of this class");
        //}

        ///// <summary>
        ///// 启动UDP服务，接受UDP消息
        ///// 使用一个套接口来监听远端终结点的连接（UDP报文）请求
        ///// 如果有连接（UDP报文）那么就开出一个线程来操作，线程中进行任务请求
        ///// socket.receivefrom(buffer , ref endpoint)函数的第二个参数为参考传入，外部必须定义并初始化，后期返回修改值，即远端结点
        ///// </summary>
        //public static void StartUDP(TextBox BaseHistory , TextBox BaseMessage)
        //{
        //    int recv;
        //    byte [] bytes = new byte[1024];
        //    //调试用，用来测试UDP数据传输的WEB下的textbox控件
        //    history = BaseHistory;
        //    message = BaseMessage;
        //    /*
        //     * 目前只针对一个连接进行处理
        //     * 这个套接口是在中心发出短信请求连接后建立来监听的
        //     * 即这个监听套接口只负责处理第一个连接到中心的终端
        //     */
        //    listenfd = new Socket(AddressFamily.InterNetwork , SocketType.Dgram , ProtocolType.Udp);

        //    IPEndPoint hostpoint = new IPEndPoint(IPAddress.Parse("219.244.93.60"), 12346);
        //    listenfd.Bind(hostpoint);

        //    history.AppendText(TimeHandle.getcurrenttime().ToString()+": \r\n"+"waiting for a client ..."+"\r\n");

        //    IPEndPoint client = new IPEndPoint(IPAddress.Any , 0);
        //    //EndPoint remote = (EndPoint)client;
        //    remotepoint = (EndPoint)client;
        //    recv = listenfd.ReceiveFrom(bytes, ref remotepoint);//接受客户端发过来的消息，并存储客户端的终结点信息
             
        //    history.AppendText(TimeHandle.getcurrenttime().ToString() + ": \r\n" + remotepoint.ToString() + "已连接到本服务器！\r\n");
        //    history.AppendText(TimeHandle.getcurrenttime().ToString()+": \r\n"+Encoding.ASCII.GetString(bytes)+"\r\n");

        //    #region  忽略多线程的情况，暂时只考虑一个套接口负责一个连接，且不再处理其他设备的连接
        //    //ParameterizedThreadStart _newoneclient = new ParameterizedThreadStart(HandleListen);
        //    //Thread newoneclient = new Thread(_newoneclient);
        //    //newoneclient.Start(remotepoint);
        //    //MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
        //    #endregion

        //    #region   测试时未分线程时部分的注释
        //    //string wel = "welcome to my test server!";
        //    //bytes = Encoding.ASCII.GetBytes(wel);
        //    //listenfd.SendTo(bytes , remote);
        //    ////listenfd.Connect();

        //    //for (; ; )
        //    //{
        //    //    bytes = new byte[1024];
        //    //    recv = listenfd.ReceiveFrom(bytes , ref remote);
        //    //    history.AppendText(TimeHandle.getcurrenttime().ToString()+": \r\n"+Encoding.ASCII.GetString(bytes , 0 , recv)+"\r\n");
        //    //    listenfd.SendTo(bytes , remote);
        //    //}
        //    #endregion
        //}

        #region   测试使用的传递参数
        //public static TextBox history;
        //public static TextBox message;
        #endregion

        #region  为构造引用类型封装的单独的类
        //private IPEndPoint hostpoint;            //服务器段即本地IPEndPoint
        //private Socket listenfd;                 //处理本次通信的套接口
        //private EndPoint remotepoint;            //本次通信连接的远端终结点
        //public static bool udpsendflag;          //有UDP数据要发送标识
        //public static bool udprecvflag;          //有UDP数据要接受标志
        //public static bool donerecvflag;         //接收完成标志
        #endregion

        public static UDPSynchField shared;         //构造出的引用类，同上注释效果
        //private int sendmsgtype;                 //要发送的信息的类型
        //private int recvmsgtype;                 //已接受的数据的类型

        /// <summary>
        /// 启动UDP服务，接受UDP消息
        /// 方法所带参数为测试显示数据所用
        /// 使用一个套接口来监听远端终结点的连接（UDP报文）请求
        /// 如果有连接（UDP报文）那么就开出一个线程来操作，线程中进行任务请求
        /// socket.receivefrom(buffer , ref endpoint)函数的第二个参数为参考传入，外部必须定义并初始化，后期返回修改值，即远端结点
        /// </summary>
        public IMServerUDP()
        {
            /*
             * 目前只针对一个连接进行处理
             * 这个套接口是在中心发出短信请求连接后建立来监听的
             * 即这个监听套接口只负责处理第一个连接到中心的终端
             */
            shared = new UDPSynchField();

            shared.listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            shared.heartfd = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            shared.hostaddress = IPAddress.Parse("219.244.93.60");
            //shared.hostpoint = new IPEndPoint(IPAddress.Parse("219.244.93.60"), 9505);

            shared.listenfd.Bind(new IPEndPoint(shared.hostaddress , 10000));
            shared.heartfd.Bind(new IPEndPoint(shared.hostaddress , 8888));

            //开启心跳包检测线程
            Thread Heartlisten = new Thread(new ThreadStart(HeartBeat.HeartCheck));
            Heartlisten.Name = "heartlisten";
            Heartlisten.Start();

            //先接收一条消息，然后开启发送和接受线程
            byte[] connectcheck = new byte[PrepareData.BUS_FRAME_MINLEN];
            IPEndPoint client = new IPEndPoint(IPAddress.Parse("219.244.93.127"), 9999);
            shared.remotepoint = (EndPoint)client;
            shared.listenfd.ReceiveFrom(connectcheck , ref shared.remotepoint);
            //MessageBox.Show("服务器接收到数据！");
            ///封装函数用于检测是否有下位机请求连接
            ///如果有：分别开启下面的两个线程
            ///如果没有：等待超时后返回错误或重新发送被请求连接包
            if (true)
            {
                //MessageBox.Show("下位机已上线！");
                ///套接口在线程外定义，主线程如果要回收套接口，先要回收子线程，然后回收套接口资源

                //开出线程单独处理接收模块
                ThreadStart _subthread = new ThreadStart(RecvMsg);
                Thread subthread = new Thread(_subthread);
                subthread.Name = "recvmsg";
                subthread.Start();

                ///开出线程单独处理发送模块
                ThreadStart _sendtread = new ThreadStart(SendMsg);
                Thread sendthread = new Thread(_sendtread);
                sendthread.Name = "sendmsg";
                sendthread.Start();

                ///看线程处理接收队列
                ThreadStart _AnalysisQueue = new ThreadStart(HandleData.AnalysisQueue);
                Thread analysisqueue = new Thread(_AnalysisQueue);
                analysisqueue.Name = "AnalysisQueue";
                analysisqueue.Start();

                ///每一秒钟检查一次数据字典清楚超时请求
                ///原计划开单独线程进行计时操作，现更改为直接开定时器
                ///ThreadStart _timecheck = new ThreadStart(TimeCheck);
                ///Thread timecheck = new Thread(_timecheck);
                ///timecheck.Name = "SendQueueTimeCheck";
                ///timecheck.Start();
                ///开出线程后主线程回到前段执行页面响应
                TimeCheck();
            }
        }

        public void TimeCheck()
        {
            TimerTick requiretimeoutcheck = new TimerTick(1000, TimerTick.UpdateTimer);
            requiretimeoutcheck.StartTimer();
        }
        /// <summary>
        /// 作为一个线程处理方法，当有连接时，为了不耽误执行完构造函数返回web端进行其他工作
        /// 接受并分类处理数据
        /// 长短帧
        /// 心跳和非心跳
        /// 标志位的操作
        /// WEB端通过事件触发置位和轮训复位进行通信操控
        /// 程序会阻塞到recievefrom()函数，所以需要开出两个线程来分别处理发送和接受
        /// 发送：循环检测发送标志位
        /// 接受：阻塞到recievefrom()
        /// </summary>
        public void InterAction()
        { 
            int recvcounter = 0;                                     //接受包计数
            int testcounter = 0;                                     //测试用误包计数
            List<int> errorcode = new List<int>();                   //测试用检测误包的误码

            int retcode = 0;                                             //帧检测返回代码
            int flag = 0;                                            //接收到数据包的数目                                   
            int recv;                                                //接收到数据的字节数
            byte[] bytes = new byte[PrepareData.BUS_FRAME_MINLEN];   //用来接收第一帧
            //byte[] exbytes= null;                                    //用来接收长帧的后续帧
            PrepareData.Msg_Bus firststruct;                         //用来接收第一帧数据转化的数据结构

            //history.AppendText(TimeHandle.getcurrenttime().ToString() + ": \r\n" + "waiting for a client ..." + "\r\n");
            ///接收一个远端终结点并进行存储
            IPEndPoint client = new IPEndPoint(IPAddress.Any, 0);

            ///这里不应该放锁，会在线程阻塞到receivefrom()导致程序崩
            lock (shared)
            {
                shared.remotepoint = (EndPoint)client;

            ///作用：测试时过滤客户端发来的测试包
            ///问题：放到下面开线程之前会导致sendmsg中消息发送不达
            ///分析：考虑开出线程之后会不会继续执行，还是因为单方面上锁没有同步
                recv = shared.listenfd.ReceiveFrom(bytes, ref shared.remotepoint); 
            }
            
            ///报文解析，看是否是UDP发来的第一个心跳包
            ///如果是，那么回送一个响应，同时返回已经通信
            ///如果等待一段一段时间没有响应，那么在此发送短信
            ///监听功能在短信发送后开启
            ///接收到第一个心跳包，则跳出此函数进行下一个方法中receive

            for (; ; )
            {
 #region 最初目的：在一个循环中实现发送和接受的轮训检测   实际效果：会阻塞到接受函数，虽然会由于心跳接受跳出阻塞执行循环  改进方法：开出单独的线程处理

                ///将发送检测放到前面，只要是不需要执行发送的情况下，都执行接收情况，因为有心跳包
                ///在接受中再检测根据标志位区分接收心跳，或者接收心跳同时接收数据
                //if (true == shared.udpsendflag)
                //{
                //    //线程锁操作,这里对值类型变量进行锁操作，考虑变量情况
                //    lock (this)
                //    {
                //        shared.udpsendflag = false;
                //        ///打包发送将要发送的数据帧
                //        string what = "hello world!";
                //        byte []_what = Encoding.ASCII.GetBytes(what);
                //        int sendcount = shared.listenfd.SendTo(_what, shared.remotepoint);
                //        MessageBox.Show("发送了"+sendcount.ToString()+"!!!");
                //        //udprecvflag = true;
                //    }
                //}

                //else
                //{   
                #endregion
                    
                    //都要接收，接收到之后在区分是心跳还是数据包，考虑这里会不会拥塞，讨论通信自身的拥塞等待机制
                    //接受客户端发过来的消息，并存储客户端的终结点信息
                    recv = shared.listenfd.ReceiveFrom(bytes, ref shared.remotepoint);
                    shared.donerecvflag = true;
                    //MessageBox.Show("发送完并已接收响应");
                    firststruct = (PrepareData.Msg_Bus)ByteStruct.BytesToStruct(bytes, typeof(PrepareData.Msg_Bus));
                    flag++;
                   // retcode = HandleData.FrameInCheck(bytes);

                //如果不是检验合格的包
                    if (33 != retcode)
                    {
                        testcounter++;
                        errorcode.Add(retcode);
                        //帧错误处理
                        //MessageBox.Show("错误代码是：" + retcode.ToString());
                    }

                //检验合格的包的处理
                    #region  添加对心跳包的处理
                    /*判断心跳             
                     * if(isheartbeat)  如果携带有效数据，则进行解析--心跳格式后续给出
                     *  listenfd.sendto(remotepoint , heartbeat);
                     * 如果是心跳帧，那么略过并回送响应
                     *这里设置一个定时器，如果连续三个心跳间隔没有收到心跳帧
                     *则认为设备掉线的一种情况
                     */
                    #endregion
                    else
                    {
                        recvcounter++;
                        //不是心跳包并且存在需要接收的数据
                        //---释放代码时考虑加锁
                        //if (true == udprecvflag && firststruct.msgType != 0x33)
                        //{
                        //    udprecvflag = false;                                  //清零标志位
                        //    //为短帧处理
                        //    if (PrepareData.BUS_FRAME_IN_DATALEN == firststruct.dataLen)
                        //    {
                        //        if (0x33 == firststruct.msgType)
                        //        {
                        //            ///如果心跳包带信息，再过解析
                        //            ///心跳包接受之后的响应，默认将接收到的包原样返回
                        //            ///心跳包还是否需要校验和检测
                        //            byte[] heart = new byte[1024];              //转接心跳包数据
                        //            listenfd.SendTo(heart, remotepoint);
                        //        }
                        //        else
                        //        {
                        //            //短数据帧处理
                        //            HandleData.ShortFrameHandle();
                        //            ///一次数据接收完成，等待WEB端轮训标志来取数据，并在web端将标志清零
                        //            ///至于数据通过哪里过渡，连接时考虑
                        //            donerecvflag = true;
                        //        }
                        //    }
                        //    //为长帧处理
                        //    else if (PrepareData.BUS_FRAME_IN_DATALEN < firststruct.dataLen)
                        //    {
                        //        //如果是长帧，那么在接收到第一帧之后，按数据长度循环接收数据
                        //        int havenrecv = 0;
                        //        int nowrecv = 0;                                      //多次发送时，每次接受到的数据的大小
                        //        byte[] bytestemp = new byte[1024];
                        //        exbytes = new byte[firststruct.dataLen + 2];
                        //        while ((havenrecv += nowrecv) < firststruct.dataLen)
                        //        {
                        //            nowrecv = listenfd.ReceiveFrom(bytestemp, ref remotepoint);
                        //            Array.Copy(bytestemp, 0, exbytes, havenrecv, nowrecv);
                        //        }
                        //        //长数据帧处理
                        //        HandleData.LongFrameHandle();
                        //        ///一次数据接收并处理完成，等待WEB端轮训标志来取数据，并在web端将标志清零
                        //        ///至于数据通过哪里过渡，连接时考虑
                        //        donerecvflag = true;
                        //    }
                        //}
                   // }
                }
            }
            #region  忽略多线程的情况，暂时只考虑一个套接口负责一个连接，且不再处理其他设备的连接
            //ParameterizedThreadStart _newoneclient = new ParameterizedThreadStart(HandleListen);
            //Thread newoneclient = new Thread(_newoneclient);
            //newoneclient.Start(remotepoint);
            //MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
            #endregion

            #region   测试时未分线程时部分的注释
            //string wel = "welcome to my test server!";
            //bytes = Encoding.ASCII.GetBytes(wel);
            //listenfd.SendTo(bytes , remotepoint);

            //for (; ; )
            //{
            //    bytes = new byte[1024];
            //    recv = listenfd.ReceiveFrom(bytes, ref remotepoint);
            //    history.AppendText(TimeHandle.getcurrenttime().ToString() + ": \r\n" + Encoding.ASCII.GetString(bytes, 0, recv) + "\r\n");
            //    listenfd.SendTo(bytes, remotepoint);
            //}
            #endregion
        }

        /// <summary>
        /// 发送UDP消息, 为独立的线程，循环检测发送标志位
        /// 不间断的检测，考虑是否添加延迟（在lock外部添加）
        /// ！注意：需加传入参数或者通过共享缓冲区
        /// </summary>
        //public void SendMsg()
        //{
        //    for (; ; )
        //    {
        //        if (true == shared.udpsendflag)
        //        {
        //            //MessageBox.Show("后台检测到有数据要发送");
        //            //线程锁操作,这里对值类型变量进行锁操作，考虑变量情况
        //            shared.udpsendflag = false;
        //            ///打包发送将要发送的数据帧
        //            int sendcount = shared.listenfd.SendTo(shared.sendbuffer, shared.remotepoint);
        //            //已经发送，开始计时
        //            this.StartTimer();
        //            //MessageBox.Show("发送了" + sendcount.ToString() + "!!!");
        //            //发送完成，置位，考虑添加发送异常处理，在异常时则不能接受
        //            shared.udprecvflag = true; 
        //        }
        //        //防止过劳
        //        Thread.Sleep(50);
        //    }
        //}

        /// <summary>
        /// 扫描发送队列发送，间隔一定的时间
        /// </summary>
        /// <param name="diff"></param>
        public void SendMsg()
        {
            for(;;)
            {
                if (Define.send_queue.Count != 0)
                {
                    Define.SendQueueItem temp = Define.send_queue.Dequeue();
                    //这里设置为-1，一直等待到套接口可写
                    if (shared.listenfd.Poll(-1, SelectMode.SelectWrite))
                    {
                        shared.listenfd.SendTo(temp.senddata, temp.destpoint);
                        //history.AppendText("新发出一条消息！\r\n");
                    }
                    else
                    {
                        //MessageBox.Show("IMServerUDP：使用poll函数等待套接口可写以便发送失败！");
                    }
                }
                //每隔500ms扫描一次套接口是否可写
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 接受UDP消息
        /// </summary>
        //public void RecvMsg()
        //{
        //    //这里如果加入发送中置位的udprecvflag检测，对于心跳考虑如何穿透（从终端向服务器心跳）
        //    for (; ; )
        //    {
        //        if (shared.udprecvflag)
        //        {
        //            shared.recvnum = shared.listenfd.ReceiveFrom(shared.recvbuffer, ref shared.remotepoint);
        //            //已经接受，停止计时
        //            this.StopTimer();
        //            shared.udprecvflag = false;
        //            shared.donerecvflag = true;
        //        }
        //        Thread.Sleep(50);
        //    }
        //}

        public void RecvMsg()
        {
            for (; ; )
            {
                if (shared.listenfd.Poll(-1, SelectMode.SelectRead))
                {
                    int recv = 0;
                    byte[] temp = new byte[1024];
                    EndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                    //当有数据需要接收时，线程工作，当没有数据需要接收时，线程会阻塞挂起
                    recv = shared.listenfd.ReceiveFrom(temp, ref ep);
                    //处理并加入到接受缓冲队列
                    Define.RecvQueueItem rqi = new Define.RecvQueueItem();
                    rqi.srcpoint = ep;
                    rqi.recvdata = new byte[recv];
                    //拷贝接收到的字节长度
                    Array.Copy(temp, 0, rqi.recvdata, 0, recv);
                    Define.recv_queue.Enqueue(rqi);
                    //history.AppendText("now message recv in queue is### " + Define.recv_queue.Count.ToString() + "\r\n");
                }
                else
                {
                    //MessageBox.Show("IMServerUDP：使用poll函数等待套接口可读以便发送失败！");
                }
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 当有一个终端到请求进行数据通信时，用于线程入口
        /// 测试处理发送接收消息
        /// </summary>
        /// <param name="temp">本线程处理的终端终结点类实例</param>
        public void HandleListen(Object current)
        {
            //MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());

            byte[] bytes = new byte[1024];
            int recv;
            EndPoint remote = current as EndPoint;
            string wel = "welcome to my test server!";
            bytes = Encoding.ASCII.GetBytes(wel);
            shared.listenfd.SendTo(bytes, remote);

            for (; ; )
            {
                bytes = new byte[1024];
                recv = shared.listenfd.ReceiveFrom(bytes, ref remote);
                //history.AppendText(TimeHandle.getcurrenttime().ToString() + ": \r\n" + Encoding.ASCII.GetString(bytes, 0, recv) + "\r\n");
                shared.listenfd.SendTo(bytes, remote);
            }
        }

        /// <summary>
        /// 关闭当前的套接口
        /// </summary>
        /// <param name="CurrentComm"></param>
        public static void CloseUDPSocket(Socket CurrentComm)
        {
            CurrentComm.Close();
        }

        /// <summary>
        /// 置发送位
        /// </summary>+
        public void ToSend()
        {
            shared.udpsendflag = true;
        }

        /// <summary>
        /// 置接受位
        /// </summary>
        public void ToRecv()
        {
            shared.udprecvflag = true;
        }

        /// <summary>
        /// 置接收完成位
        /// </summary>
        public void RecvDone()
        {
            shared.donerecvflag = true;
        }

        #region  原始的aftersend方法
        /// <summary>
        /// 发送之后的一切处理过程
        /// aftersend方法中的error和errornum等需要外部交互的变量在整合是调出
        /// </summary>
        /// <param name="comstruct">发送数据的核对留样</param>
        /// <param name="sender">各类数据帧组织所需要的数据结构（ushort[]、dictionary<string , string>）
        ///                      没有专门设置一个缓冲队列来处理</param>
        /// <param name= "downwrite">专门为文件的读写做已读字节的标注，即下次开始读写的位置</param>
        /// <return><\return>
        /*public static void AfterSend(PrepareData.Compare comstruct, object sender , ushort donewrite)
        {
            ushort errornum = 0;
            Dictionary<ushort, ushort> error = new Dictionary<ushort, ushort>();

            byte[] datatemp;
            while (shared.donerecvflag != true)
            {   
                Thread.Sleep(50);
            }

            shared.donerecvflag = false;
            MessageBox.Show("发送后接收到返回帧！");
            datatemp = new byte[shared.recvnum];
            Array.Copy(shared.recvbuffer , 0 , datatemp , 0 , shared.recvnum);

            #region 开始写处理数据方法之前的想法
            ///首先拿读操作单元做例子
            ///相应的方法
            ///提取数据包传入相应的方法
            ///检测数据包的合法性（错误否，请求相应是否一致）
            ///数据如可，并通过标志位通知WEB
            #endregion

            int retcode;
            int frametype = HandleData.TypeofFrame(datatemp).frametype;

            if (0 == frametype)
            {
                retcode = HandleData.FrameInCheck(datatemp, comstruct);
            }
            else
                if (1 == frametype)
                {
                    retcode = HandleData.FrameOutCheck(datatemp, comstruct);
                }
                else
                {
                    retcode = 0;
                    MessageBox.Show("长帧和短帧类型判断错误！");
                }

            if (33 == retcode)
            {
                switch (comstruct.msgType)
                {
                        //通信测试
                    case (byte)MSGEncoding.MsgType.LoopBack:
                        {
                            //对于loopback来说的sender为发送下去的通信测试帧的CRC校验码=>改为发送的字节数组原始数据
                            //通过比对CRC校验码（返回）=>不如直接字节数组比较方便
                            byte[] rawdata = (byte[])sender;
                            bool result = LoopBack.MsgHandle(datatemp , rawdata);
                        }
                        break;
                        //读操作单元
                    case (byte)MSGEncoding.MsgType.ReadUnit:
                        {
                            //目前来看公用error和errornum
                            Dictionary<ushort, object> hello = ReadUnit.MsgHandle((ushort[])sender, comstruct,
                                                       datatemp, ref errornum, error);
                            history.AppendText("做出的请求响应为：\r\n");
                            for (int i = 0; i < hello.Count; i++)
                            {
                                history.AppendText(hello.ElementAt(i).Key.ToString() + " : " + hello.ElementAt(i).Value.ToString() + "\r\n");
                            }
                        }
                        break;
                        //写操作单元
                    case (byte)MSGEncoding.MsgType.WriteUnit:
                        {
                            //通过提取转换得来的操作单元的数组
                            ushort[] unit = new ushort[10];
                            int result = WriteUnit.MsgHandle(error , ref errornum , datatemp , unit);
                            if (1 == result)
                            {
                                MessageBox.Show("写操作单元成功！");
                            }
                            else 
                            {
                                MessageBox.Show("写操作单元存在错误！");
                            }
                        }
                        break;
                        //读文件
                    case (byte)MSGEncoding.MsgType.ReadFile:
                        {
                            //交互过程
                            ushort Error = 0;
                            ushort index = 0;
                            ReadFile.Parameter com = (ReadFile.Parameter)sender;
                            //这些在第一次组包的时候都已初始化
                            string path = "s:\\" + com.filename + ".txt";
                            //com.start = 0;
                            //com.readbytes = 0;
                            FileStream filerecv = new FileStream(path , FileMode.OpenOrCreate , FileAccess.Write);
                        LoopRead:
                            int thisdone = ReadFile.MsgHandle(filerecv, datatemp, ref Error);
                            //如果写入0字节，那么有两种情况：A，文件读取完毕（error为0）B，文件读取出错（error不为0）
                            if (0 == thisdone)
                            {
                                //文件读取完成
                                if (0 == Error)
                                {
                                    filerecv.Close();
                                    //MessageBox.Show("文件读取完成！");
                                    goto ReadRightQuit;
                                }
                                //文件读取出错
                                else 
                                {
                                    //MessageBox.Show("读取文件时出错！");
                                    goto ReadWrongQuit;
                                }
                            }
                            //如果读取到大于0的字节，说明正在读取文件，如果读取到小于指定读取字节个数，应该是文件读完，
                            //但是为了统一写文件，这个交互之后再次发送请求，下面解析到请求读取超过文件长度的信息，
                            //会返回文件读取结束（读到0字节并且error=0）
                            else
                            {
                                index += (ushort)thisdone;
                                com.start = index;
                                com.readbytes = ReadFile.MAX;
                                IMServerUDP.shared.sendbuffer = PrepareData.Packetization(comstruct , com , ref donewrite);
                                IMServerUDP.shared.udpsendflag = true;
                                IMServerUDP.shared.udprecvflag = true;

                                while (shared.donerecvflag != true)
                                {
                                    Thread.Sleep(50);
                                }
                                shared.donerecvflag = false;
                                MessageBox.Show("读文件过程接收到后续帧！");
                                datatemp = new byte[IMServerUDP.shared.recvnum];
                                Array.Copy(IMServerUDP.shared.recvbuffer, 0, datatemp, 0, IMServerUDP.shared.recvnum);
                                frametype = HandleData.TypeofFrame(datatemp);
                                if (0 == frametype)
                                {
                                    retcode = HandleData.FrameInCheck(datatemp, comstruct);
                                }
                                else
                                    if (1 == frametype)
                                    {
                                        retcode = HandleData.FrameOutCheck(datatemp, comstruct);
                                    }
                                    else
                                    {
                                        retcode = 0;
                                        goto ReadWrongQuit;
                                        //MessageBox.Show("读文件过程的后续帧的长帧和短帧类型判断错误！");
                                    }
                                if (33 == retcode)
                                {
                                    goto LoopRead;
                                }
                                else
                                {
                                    //MessageBox.Show("读文件过程后续帧的校验存在错误!");
                                    goto ReadWrongQuit;
                                }
                            }
                        ReadWrongQuit:
                            {
                                MessageBox.Show("读文件过程存在错误：校验、读取错误等");
                                goto ReadQuit;
                            }
                        ReadRightQuit:
                            {
                                MessageBox.Show("读文件过程完成！");
                                goto ReadQuit;
                            }
                        ReadQuit:
                            {
                                MessageBox.Show("度文件过程从AfterSend方法跳出！");
                        }
                        }
                        break;
                        //写文件
                    case (byte)MSGEncoding.MsgType.WriteFile:
                        {
                            //写文件的交互过程，需要传结构体，结构体中数据需要返回并有这里参考循环
                            //本函数中传入的donewrite为after之前packet中读取的字节数
                            ushort errorcode = 0;
                            WriteFile.Parameter para = (WriteFile.Parameter)sender;
                            //返回帧中上送的下位机写入的字节数
                        LoopWrite:
                            ushort done = WriteFile.MsgHandle(datatemp , ref errorcode);
                            if (0 != errorcode)
                            {
                                //有错误发生时的响应帧
                                goto WriteWrongQuit;
                                //MessageBox.Show("写文件过程有错误发生！");
                            }
                            else
                            {
                                //服务器发送 文件写 结束帧后下位机的正常响应
                                if (0 == done)
                                {
                                    //正常结束
                                    goto WriteRightQuit;
                                }
                                //正常响应下位机接收到服务器的文件数据并写入的正常响应
                                else
                                {
                                    if (done != donewrite)
                                    {
                                        goto WriteWrongQuit;//传下的字节数和真正写入的字节数不一致（各种检测机制保证一致，这里预防）
                                    }
                                    else
                                    {
                                        //只修改读文件的起始位置，读取的字节数通过加入的loopback方法测试得到
                                        para.start += done;
                                        IMServerUDP.shared.sendbuffer = PrepareData.Packetization(comstruct, para , ref donewrite);
                                        IMServerUDP.shared.udpsendflag = true;
                                        IMServerUDP.shared.udprecvflag = true;
                                        while (!IMServerUDP.shared.donerecvflag)
                                        {
                                            Thread.Sleep(50);
                                        }

                                        MessageBox.Show("写文件过程接收到后续帧！");
                                        datatemp = new byte[IMServerUDP.shared.recvnum];
                                        Array.Copy(IMServerUDP.shared.recvbuffer, 0, datatemp, 0, IMServerUDP.shared.recvnum);
                                        retcode = HandleData.FrameInCheck(datatemp, comstruct);
                                        if (33 == retcode)
                                        {
                                            goto LoopWrite;//接收到了数据，并判断还未发送完文件，循环处理
                                        }
                                        else
                                        {
                                            goto WriteWrongQuit;
                                        }
                                    }
                                }
                            }
                        WriteWrongQuit:
                            {
                                MessageBox.Show("写文件过程可能为后续帧错误，或者校验错误或者。。。");
                                goto WriteQuit;
                            }
                        WriteRightQuit:
                            {
                                MessageBox.Show("写文件过程成功完成！");
                                goto WriteQuit;
                            }
                        WriteQuit:
                            {
                                MessageBox.Show("写文件过程从Aftersend中退出！");
                            }
                        }
                        break;
                        //获取文件信息
                        //独属于获取文件信息的错误代码
                    case (byte)MSGEncoding.MsgType.GetFileInfo:
                        {
                            ushort sorterror = 0;
                            Dictionary<long, ushort> fileinfo = GetFileInfo.MsgHandle(ref sorterror , datatemp);
                            MessageBox.Show("获取文件信息处理完成！");
                        }
                        break;
                        //读缓冲区
                    case (byte)MSGEncoding.MsgType.ReadBuffer:
                        {
                            //根据请求的数据帧的子类型确定返回值
                            object result = ReadBuffer.MsgHandle(datatemp , comstruct.msgSubType);
                        }
                        break;
                        //获取错误信息
                        //查询出现错误情况，后期结合具体情况讨论
                    case (byte)MSGEncoding.MsgType.GetErrorInfo:
                        {
                            string errorexplain = GetErrorInfo.MsgHandle(datatemp);
                            if (null == errorexplain)
                            {
                                MessageBox.Show("获取错误信息失败，内容上的失败，因为已经经过校验");
                            }
                            else
                            {
                                MessageBox.Show("获取错误信息成功！");
                            }
                        }
                        break;
                    default:
                        {
                            MessageBox.Show("接收帧的类型错误！");
                        }
                        break;
                }

            }
            else
            {
                MessageBox.Show("接收的数据帧存在错误！");
            }

        }*/
        #endregion
    }
}
