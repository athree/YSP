using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMserver.CommonFuncs;
using System.Net;
using System.Net.Sockets;

namespace IMserver.SubFuncs
{
    public class WriteUnit
    {
        /// <summary>
        /// 控制设备
        /// </summary>
        /// <param name="_frame">引用结构体</param>
        /// <param name="destid">目的ID</param>
        /// <param name="data">内容</param>
        /// <returns>组装好的字节数组</returns>
        //控制设备
        public static byte[] ControlDev(byte destid , Dictionary<string , string> datadict)
        {
            byte[] donedata;
            byte[] data = GetData(datadict);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.WriteUnit;
            _frame.msgSubType = (byte)MSGEncoding.WriteUint.ControlDev;
            _frame.dataLen = (ushort)data.Length;
            //长短包的区分和数据组合在MergeMsg（）方法中完成
            donedata = PrepareData.MergeMsg(ref _frame , data);
            return donedata;
        }
        //重载
        public static byte[] ControlDev(byte destid, Dictionary<ushort, object> datadict)
        {
            byte[] donedata;
            byte[] data = GetData(datadict);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.WriteUnit;
            _frame.msgSubType = (byte)MSGEncoding.WriteUint.ControlDev;
            _frame.dataLen = (ushort)data.Length;
            //长短包的区分和数据组合在MergeMsg（）方法中完成
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }

        //设置配置信息
        public static byte[] Setcfg(byte destid, Dictionary<string, string> datadict)
        {
            byte[] donedata;
            byte[] data = GetData(datadict);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.WriteUnit;
            _frame.msgSubType = (byte)MSGEncoding.WriteUint.Setcfg;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame , data);
            return donedata; 
        }

        public static byte[] Setcfg(byte destid, Dictionary<ushort, object> datadict)
        {
            byte[] donedata;
            byte[] data = GetData(datadict);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.WriteUnit;
            _frame.msgSubType = (byte)MSGEncoding.WriteUint.Setcfg;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }

        //写数据
        public static byte[] WriteData(byte destid, Dictionary<string, string> datadict)
        {
            byte[] donedata;
            byte[] data = GetData(datadict);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.WriteUnit;
            _frame.msgSubType = (byte)MSGEncoding.WriteUint.WriteData;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame , data);
            return donedata;
        }

        public static byte[] WriteData(byte destid, Dictionary<ushort, object> datadict)
        {
            byte[] donedata;
            byte[] data = GetData(datadict);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.WriteUnit;
            _frame.msgSubType = (byte)MSGEncoding.WriteUint.WriteData;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }
        
        /// <summary>
        /// 获取下设帧数据域内容
        /// 数据字典的键需要拆分成标志和类型（以空格隔开），方便组织
        /// ！注意：dictionary的可以通过参数传递或者共享同步变量得来
        /// </summary>
        /// <param name="unitvalue"> web端传来的消息</param>
        /// <returns>数据域</returns>
        public static byte[] GetData(Dictionary<string , string> unitvalue)
        {
            byte [] data;
            string [] collect;
            string flag;
            ushort unit;
            string value;
            //方便动态组织数据
            List<byte> datalist = new List<byte>();
            for (int i = 0; i < unitvalue.Count; i++)
            {
                collect = unitvalue.ElementAt(i).Key.Split(' ');
                flag = collect[0];
                unit = (ushort)Convert.ToUInt16(collect[1]);
                value = unitvalue.ElementAt(i).Value;
                //单数据类型处理
                if ("o" == flag)
                {
                    //collect[]: collect[0]=大类数据类型   collect[1] = 操作单元
                    switch (unit)
                    {
                        //uchar处理
                        case 0:
                            {
                                //每次循环中只会操作一次unit，下次循环unit会被更新(所以忽略这里的修改)
                                //datalist.Add((byte)unit);
                                //datalist.Add((byte)(unit>>8));
                                AddUnitToList(datalist , unit);
                                datalist.Add((byte)Convert.ToUInt16(value));
                            }
                            break;
                        //ushort处理
                        case 1:
                            {
                                AddUnitToList(datalist , unit);
                                AddUshortToList(datalist , Convert.ToUInt16(value));
                                //datalist.Add((byte)unit);
                                //datalist.Add((byte)(unit>>8));
                                //ushort temp = Convert.ToUInt16(value);
                                //datalist.Add((byte)temp);
                                //datalist.Add((byte)(temp >> 8));
                            }
                            break;
                        //float处理
                        case 2:
                            {
                                //float转换为字节数组
                                //float temp = Convert.ToSingle(value);
                                //byte[] arrtemp = ByteStruct.StructToBytes(temp);
                                AddUnitToList(datalist , unit);
                                AddFloatToList(datalist , Convert.ToSingle(value));
                                //datalist.Add((byte)unit);
                                //datalist.Add((byte)(unit >> 8));
                                //datalist.Add(arrtemp[0]);
                                //datalist.Add(arrtemp[1]);
                                //datalist.Add(arrtemp[2]);
                                //datalist.Add(arrtemp[3]);
                            }
                            break;
                        //datetime处理
                        case 3:
                            {
                                //datalist.Add((byte)unit);
                                //datalist.Add((byte)(unit >> 8));
                                //datetime转换成long来移位保存
                                AddUnitToList(datalist , unit);
                                long date = TimerTick.TimeSpanToSecond(Convert.ToDateTime(collect[2]));
                                datalist.Add((byte)date);
                                datalist.Add((byte)(date >> 8));
                                datalist.Add((byte)(date >> 16));
                                datalist.Add((byte)(date >> 24));
                                datalist.Add((byte)(date >> 32));
                                datalist.Add((byte)(date >> 40));
                                datalist.Add((byte)(date >> 48));
                                datalist.Add((byte)(date >> 56));
                            }
                            break;
                        default:
                            //MessageBox.Show("在解析为单类型数据后没有对应的类型");
                            break;
                    }
                }
                //单类型数组处理
                else if ("s" == flag)
                {
                    switch (unit)
                    {
                        #region   //组织变电站名和网络地址
                        //这里在解析相对数据的时候过滤字符串结束符
                        //case 4:
                        //    {
                        //        //datalist.Add((byte)unit);
                        //        //datalist.Add((byte)(unit >> 8));
                        //        if (16 == MyDictionary.unitlendict[unit])
                        //        {
                        //            AddUnitToList(datalist, unit);
                        //            byte []temp_16 = new byte[16];
                        //            Encoding.Unicode.GetBytes(value).CopyTo(temp_16 , 0);
                        //            for (int j = 0; j < 16; j++)
                        //            {
                        //                datalist.Add(temp_16[j]);
                        //            }
                        //        }
                        //        else
                        //            if (80 == MyDictionary.unitlendict[unit])
                        //            {
                        //                AddUnitToList(datalist, unit);
                        //                byte []temp_80 = new byte[80];
                        //                Encoding.Unicode.GetBytes(value).CopyTo(temp_80 , 0);
                        //                for (int j = 0; j < 80; j++)
                        //                {
                        //                    datalist.Add(temp_80[j]);
                        //                }
                        //            }
                        //            else
                        //                MessageBox.Show("变电站名和软件版本类超出范围");
                        //    }
                        //    break;
                        //组织网络通信地址
                        //传来为ip地址字符串形式，这里不需要对数据点表中给定的uchar数组类型进行分治处理
                        //case 5:
                        //    {
                        //        //datalist.Add((byte)unit);
                        //        //datalist.Add((byte)(unit >> 8));
                        //        AddUnitToList(datalist , unit);
                        //        byte []iparray = IPAddress.Parse(value).GetAddressBytes();
                        //        datalist.Add(iparray[0]);
                        //        datalist.Add(iparray[1]);
                        //        datalist.Add(iparray[2]);
                        //        datalist.Add(iparray[3]);
                        //    }
                        //    break;
                        //ushort[]数组处理，只有ushort[4]类型
                        //数据字典中这类数据的组织：
                        //(A) 118+ushort    (B) 118+ushort  (C) 118+ushort   (D) 118+ushort 括号中不存在
                        //一个数组的四个数据连续组织，装包是检测到第一个顺序装入
                        //解析的时候做同样处理
#endregion
                        case 5:
                            {
                                //两个ushort元素数组处理
                                if (2 == MyDictionary.unitlendict[unit])
                                {
                                    //给出开头顺序后去一个ushort
                                    AddUnitToList(datalist , unit);
                                    AddUshortToList(datalist , Convert.ToUInt16(value));
                                    AddUshortToList(datalist , Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                                    ///datalist.Add((byte)unit);
                                    ///datalist.Add((byte)(unit >> 8));
                                    ///ushort temp1 = Convert.ToUInt16(value);
                                    ///datalist.Add((byte)temp1);
                                    ///datalist.Add((byte)(temp1 >> 8));
                                    ///i++;
                                    ///temp1 = Convert.ToUInt16(unitvalue.ElementAt(i).Value);
                                    ///datalist.Add((byte)temp1);
                                    ///datalist.Add((byte)(temp1 >> 8));
                                }
                                //四个ushort元素数组处理
                                else
                                    if (4 == MyDictionary.unitlendict[unit])
                                    {
                                        AddUnitToList(datalist, unit);
                                        AddUshortToList(datalist, Convert.ToUInt16(value));
                                        AddUshortToList(datalist, Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                                        AddUshortToList(datalist, Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                                        AddUshortToList(datalist, Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                                        //datalist.Add((byte)unit);
                                        //datalist.Add((byte)(unit >> 8));
                                        //ushort temp2 = Convert.ToUInt16(value);
                                        //datalist.Add((byte)temp2);
                                        //datalist.Add((byte)(unit >> 8));
                                        //for (int j = 0; j < 3; j++)
                                        //{
                                        //    i++;
                                        //    temp2 = Convert.ToUInt16(unitvalue.ElementAt(i).Value);
                                        //    datalist.Add((byte)temp2);
                                        //    datalist.Add((byte)(temp2 >> 8));
                                        //}
                                    }
                                    else
                                    {
                                        //MessageBox.Show("writeunit_getdata:ushort类型单数组格式出现范围之外数组");
                                    }
                            }
                            break;
                        //float类数组处理
                        case 6:
                            {
                                if (MyDictionary.unitlendict[unit] == 2)
                                {
                                    AddUnitToList(datalist , unit);
                                    AddFloatToList(datalist , Convert.ToSingle(value));
                                    AddFloatToList(datalist , Convert.ToSingle(unitvalue.ElementAt(++i).Value));
                                }
                                else
                                    if (MyDictionary.unitlendict[unit] == 3)
                                    {
                                        AddUnitToList(datalist , unit);
                                        AddFloatToList(datalist, Convert.ToSingle(unitvalue.ElementAt(++i).Value));
                                        AddFloatToList(datalist, Convert.ToSingle(unitvalue.ElementAt(++i).Value));
                                    }
                                    else
                                        if (MyDictionary.unitlendict[unit] == 5)
                                        {
                                            AddUnitToList(datalist, unit);
                                            AddFloatToList(datalist, Convert.ToSingle(unitvalue.ElementAt(++i).Value));
                                            AddFloatToList(datalist, Convert.ToSingle(unitvalue.ElementAt(++i).Value));
                                            AddFloatToList(datalist, Convert.ToSingle(unitvalue.ElementAt(++i).Value));
                                            AddFloatToList(datalist, Convert.ToSingle(unitvalue.ElementAt(++i).Value));
                                        }
                                        else
                                        {
                                            //MessageBox.Show("float类型单数组格式出现范围之外的数组");
                                        }
                            }
                            break;
                        default:
                            {
                                //MessageBox.Show("单类型数组处理错误！");
                                break;
                            }

                    }
                }
                #region  //混合类型处理
                //都是数据传输接口相关参数，暂时忽略
                //else if ("x" == flag)
                //{
                //    if (157 == unit || 160 == unit)
                //    {
                //        AddUnitToList(datalist , unit);
                //        AddIPToList(datalist , value);
                //        AddUshortToList(datalist , Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                //    }
                //    else
                //        if (158 == unit || 161 == unit)
                //        {
                //            AddUnitToList(datalist, unit);
                //            AddUshortToList(datalist, Convert.ToUInt16(value));
                //            datalist.Add((byte)Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                //            datalist.Add((byte)Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                //            datalist.Add((byte)Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                //        }
                //        else
                //            if (159 == unit || 162 == unit)
                //            {
                //                AddUnitToList(datalist, unit);
                //                AddIPToList(datalist, value);
                //                AddUshortToList(datalist, Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                //                AddIPToList(datalist, unitvalue.ElementAt(++i).Value);
                //                AddIPToList(datalist, unitvalue.ElementAt(++i).Value);
                //            }
                //            else
                //            {
                //                MessageBox.Show("混合类型处理超出可行范围");
                //            }
                //}
                #endregion
                else
                {
                    //MessageBox.Show("解析完成字符串数据大类标志报错");
                }
            }
            data = datalist.ToArray<byte>();
            return data;
        }

        public static byte[] GetData(Dictionary<ushort, object> unitvalue)
        {
            byte[] data;
            //方便动态组织数据
            List<byte> datalist = new List<byte>();
            foreach (KeyValuePair<ushort, object> kvp in unitvalue)
            {
                ushort unit = kvp.Key;
                object value = kvp.Value;
                ushort DS_type = MyDictionary.unittypedict[unit];
                switch(DS_type)
                {
                    //uchar
                    case 0:
                        {
                            AddUshortToList(datalist , unit);
                            datalist.Add((byte)value);
                            break;
                        }
                    //ushort
                    case 1:
                        {
                            AddUshortToList(datalist, unit);
                            AddUshortToList(datalist , (ushort)value);
                            break;
                        }
                    //float
                    case 2:
                        {
                            AddUshortToList(datalist, unit);
                            AddFloatToList(datalist , (float)value);
                            break;
                        }
                    //datetime
                    case 3:
                        {
                            AddUshortToList(datalist, unit);
                            AddTimeToList(datalist , (DateTime)value);
                            break;
                        }
                    //ushort[2]  EraseRange
                    case 4:
                    //ushort[4]  GasFixPara_A
                    case 5:
                    //ushort[4]  GasFixPara_B
                    case 6:
                    //float[2]   EnvironmentSetting
                    case 7:
                    //float[3]   H2OAnlyParam_AW
                    case 8:
                    //float[3]   H2OAnlyParam_T
                    case 9:
                    //float[5]   GasFixParameters
                    case 10:
                        {
                            AddUshortToList(datalist, unit);
                            byte[] temp = ByteStruct.StructToBytes(value);
                            AddBytesToList(datalist , temp);
                            break;
                        }
                    default:
                        {
                            //MessageBox.Show("writeunit-getdata:数据类型出错！");
                            break;
                        }
                }
            }
            #region  //混合类型处理
                //都是数据传输接口相关参数，暂时忽略
                //else if ("x" == flag)
                //{
                //    if (157 == unit || 160 == unit)
                //    {
                //        AddUnitToList(datalist , unit);
                //        AddIPToList(datalist , value);
                //        AddUshortToList(datalist , Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                //    }
                //    else
                //        if (158 == unit || 161 == unit)
                //        {
                //            AddUnitToList(datalist, unit);
                //            AddUshortToList(datalist, Convert.ToUInt16(value));
                //            datalist.Add((byte)Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                //            datalist.Add((byte)Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                //            datalist.Add((byte)Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                //        }
                //        else
                //            if (159 == unit || 162 == unit)
                //            {
                //                AddUnitToList(datalist, unit);
                //                AddIPToList(datalist, value);
                //                AddUshortToList(datalist, Convert.ToUInt16(unitvalue.ElementAt(++i).Value));
                //                AddIPToList(datalist, unitvalue.ElementAt(++i).Value);
                //                AddIPToList(datalist, unitvalue.ElementAt(++i).Value);
                //            }
                //            else
                //            {
                //                MessageBox.Show("混合类型处理超出可行范围");
                //            }
                //}
                #endregion
            data = datalist.ToArray<byte>();
            return data;
        }

        /// <summary>
        /// 将无符号短整型类数据加入链表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        public static void AddUshortToList(List<byte> list , ushort value) 
        {
            ushort temp = Convert.ToUInt16(value);
            list.Add((byte)temp);
            list.Add((byte)(temp >> 8));
        }

        /// <summary>
        /// 将单精度浮点数据加入链表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        public static void AddFloatToList(List<byte> list , float value)
        {
            byte[] arrtemp = ByteStruct.StructToBytes(value);
            list.Add(arrtemp[0]);
            list.Add(arrtemp[1]);
            list.Add(arrtemp[2]);
            list.Add(arrtemp[3]);
        }

        /// <summary>
        /// 将数据单元编号加入到链表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="unitnum"></param>
        public static void AddUnitToList(List<byte> list, ushort unitnum)
        {
            list.Add((byte)unitnum);
            list.Add((byte)(unitnum >> 8));
        }

        /// <summary>
        /// 添加时间到队列
        /// </summary>
        /// <param name="list"></param>
        /// <param name="time"></param>
        public static void AddTimeToList(List<byte> list, long time)
        {
            byte[] _time = ByteStruct.StructToBytes(time);
            list.Add(_time[0]);
            list.Add(_time[1]);
            list.Add(_time[2]);
            list.Add(_time[3]);
            list.Add(_time[4]);
            list.Add(_time[5]);
            list.Add(_time[6]);
            list.Add(_time[7]);
        }

        /// <summary>
        /// 添加时间到列表，重载datetime
        /// </summary>
        /// <param name="list"></param>
        /// <param name="time"></param>
        public static void AddTimeToList(List<byte> list, DateTime time)
        {
            long _time1 = TimerTick.TimeSpanToSecond(time);
            byte[] _time2 = ByteStruct.StructToBytes(_time1);
            list.Add(_time2[0]);
            list.Add(_time2[1]);
            list.Add(_time2[2]);
            list.Add(_time2[3]);
            list.Add(_time2[4]);
            list.Add(_time2[5]);
            list.Add(_time2[6]);
            list.Add(_time2[7]);
        }

        /// <summary>
        /// 添加IP类数据到连表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="str"></param>
        public static void AddIPToList(List<byte> list, string str)
        {
            byte[] iparray = IPAddress.Parse(str).GetAddressBytes();
            list.Add(iparray[0]);
            list.Add(iparray[1]);
            list.Add(iparray[2]);
            list.Add(iparray[3]);
        }

        /// <summary>
        /// 将字节数组添加到单链表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="bytes"></param>
        public static void AddBytesToList(List<byte> list , byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                list.Add(bytes[i]);
            }
        }
        /// <summary>
        /// 写操作单元响应处理
        /// </summary>
        /// <param name="error">错误的数目</param>
        /// <param name="errornum">错误列表</param>
        /// <param name="b_recvframe">接受到的原始数据帧（接受的全部字节）</param>
        /// <returns></returns>
        public static int MsgHandle(Dictionary<ushort , ushort> error , ref ushort errornum , byte[] b_recvframe , ushort[] unit)
        {
            switch(HandleData.TypeofFrame(b_recvframe).frametype)
            {
                    //接收到的是长帧
                case 1:
                    {
                        byte[] onlydata = new byte[b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN - 2];
                        Array.Copy(b_recvframe, PrepareData.BUS_FRAME_MINLEN, onlydata, 0, onlydata.Length);
                        TransInfo(error, ref errornum, onlydata, unit);
                        break;
                    }
                    //接收到的是短帧
                case 0:
                    {
                        byte[] onlydata = ((PrepareData.Msg_Bus)ByteStruct.BytesToStruct(b_recvframe , typeof(PrepareData.Msg_Bus))).data; 
                        TransInfo(error , ref errornum , onlydata , unit);
                        break;
                    }
                    //判断长短帧错误
                default:
                    {
                        break;
                    }
            }
            //写操作单元完成
            if(0 == errornum)
            {
                return 1;
            }
            //写操作单元存在错误
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 数据域解析
        /// </summary>
        /// <param name="error">错误列表</param>
        /// <param name="errornum">错误数</param>
        /// <param name="recvdata">执行状态字和错误代码（如果有错误代码）</param>
        public static void TransInfo(Dictionary<ushort, ushort> error, ref ushort errornum, byte[] recvdata , ushort[] unit)
        {
            int m = 0;                   //返回数据字节数组的下标
            
            //操作单元的个数，这里是从发送帧中间接得来
            int basenum = unit.Length;
            int remainder = basenum % 8;

            //操作单元数组的下标
            int index = 0;    
           
            //发来字节中的执行状态字所占字节数
            int basebyte = (int)Math.Ceiling(basenum / 8.0);

            //存放执行状态字
            byte[] b_basebyte = new byte[basebyte];
            Array.Copy(recvdata, 0, b_basebyte, 0, basebyte);

            //返回的值数据信息（不包括执行状态字）
            byte[] onlydata = new byte[recvdata.Length - basebyte];
            Array.Copy(recvdata, basebyte, onlydata, 0, recvdata.Length - basebyte);

            //全部正确的情况下，返回的执行状态字值
            //这里如果需要提高效率，可以事先算出执行状态字全1的情况下值与理论个数的执行状态字的数值比较
            //如果数值对应即可省去下面的扫描

            //只要传入需要解析的数据、操作单元数组、余数
            for (int i = basebyte - 1; i >= 0; i--)
            {
                //处理如果存在少于8个执行状态字的首个字节
                if (0 == i) //如果I等于0，那么执行状态子当前在第一字节，即可能存在不满8个执行状态字情况
                {
                    for (int j = 0; j  <= remainder - 1; j++)
                    {
                        //对应操作单元的执行状态字是否成功
                        int result = b_basebyte[i] & (byte)(int)Math.Pow(2, j);

                        //请求的操作单元返回错误
                        if (0 == result)
                        {
                            //获取错误代码
                            byte[] temp = new byte[2];
                            Array.Copy(onlydata, m%8, temp, 0, 2);

                            //推移移动的位数
                            m += 2;
                            ushort errorcode = (ushort)ByteStruct.BytesToStruct(temp, typeof(ushort));
                            //errorcode处理
                            errornum++;
                            error.Add(unit[index], errorcode);
                            //index++;
                        }
                        index++;
                    }
                    //只处理一遍
                    //remainder = 0;
                }
                else
                {
                    for (int j = 0; j <= 8-1; j++)
                    {
                        int result = b_basebyte[i] & (byte)(int)Math.Pow(2, j);

                        //请求的操作单元返回错误
                        if (0 == result)
                        {
                            //获取错误代码
                            byte[] temp = new byte[2];
                            Array.Copy(onlydata, m%8, temp, 0, 2);
                            //推移移动的位数
                            m += 2;
                            ushort errorcode = (ushort)ByteStruct.BytesToStruct(temp, typeof(ushort));
                            //errorcode处理
                            errornum++;
                            error.Add(unit[index], errorcode);
                            //index++;
                        }
                        index++;
                    }
                }
            }
        }
        
        /// <summary>
        /// 前台数据字典传下到通信识别数据字典的转换
        /// 假若传入的为字符串数组（手动转为其他类型数组），默认为数组没有问题
        /// </summary>
        /// <param name="rawdict">前台传递的<string , string>数据字典</param>
        /// <returns>原定方法接收的类型数据字典</returns>
        public static Dictionary<ushort, object> TransDict(Dictionary<string, object> rawdict)
        {
            Dictionary<ushort, object> retdict = new Dictionary<ushort, object>();
            foreach(KeyValuePair<string , object> kvp in rawdict)
            {
                ushort newkey = ushort.Parse(kvp.Key);
                ushort value_len = MyDictionary.unitlendict[newkey];
                switch (MyDictionary.unittypedict[newkey])
                {
                    //char[]:一个变电站名一个软件版本，但软件版本为只读，这里只做[80]
                    //一个string，非string[]
                    case 4:
                        {
                            byte[] _temp = Encoding.ASCII.GetBytes((string)kvp.Value);
                            byte[] temp = new byte[80];
                            _temp.CopyTo(temp , 0);
                            retdict.Add(newkey , temp);
                            break;
                        }
                    //ushort[2]
                    case 5:
                        {
                            Define.EraseRange temp = new Define.EraseRange();
                            temp.start = ushort.Parse(((string[])kvp.Value)[0]);
                            temp.end = ushort.Parse(((string[])kvp.Value)[1]);
                            retdict.Add(newkey , temp);
                            break;
                        }
                    //ushort[4]
                    case 6:
                        {
                            Define.GasFixPara_A temp = new Define.GasFixPara_A();
                            temp.PeakPoint = ushort.Parse(((string[])kvp.Value)[0]);
                            temp.PeakLeft = ushort.Parse(((string[])kvp.Value)[1]);
                            temp.PeakRight = ushort.Parse(((string[])kvp.Value)[2]);
                            temp.PeakWidth = ushort.Parse(((string[])kvp.Value)[3]);
                            retdict.Add(newkey , temp);
                            break;
                        }
                    //ushort[4]
                    case 7:
                        {
                            Define.GasFixPara_B temp = new Define.GasFixPara_B();
                            temp.Left_YMin = ushort.Parse(((string[])kvp.Value)[0]);
                            temp.Left_XMax = ushort.Parse(((string[])kvp.Value)[1]);
                            temp.Right_YMin = ushort.Parse(((string[])kvp.Value)[2]);
                            temp.Right_XMax = ushort.Parse(((string[])kvp.Value)[3]);
                            retdict.Add(newkey, temp);
                            break;
                        }
                    //float[2]
                    case 8:
                        {
                            Define.EnvironmentSetting temp = new Define.EnvironmentSetting();
                            temp.oilFactorA = float.Parse(((string[])kvp.Value)[0]);
                            temp.oilFactorB = float.Parse(((string[])kvp.Value)[1]);
                            retdict.Add(newkey , temp);
                            break;
                        }
                    //float[3]
                    case 9:
                        {
                            Define.H2OAnlyParam_AW temp = new Define.H2OAnlyParam_AW();
                            temp.H2OAnlyParam_AW_A = float.Parse(((string[])kvp.Value)[0]);
                            temp.H2OAnlyParam_AW_K = float.Parse(((string[])kvp.Value)[1]);
                            temp.H2OAnlyParam_AW_B = float.Parse(((string[])kvp.Value)[2]);
                            retdict.Add(newkey , temp);
                            break;
                        }
                    //float[3]
                    case 10:
                        {
                            Define.H2OAnlyParam_T temp = new Define.H2OAnlyParam_T();
                            temp.H2OAnlyParam_T_A = float.Parse(((string[])kvp.Value)[0]);
                            temp.H2OAnlyParam_T_K = float.Parse(((string[])kvp.Value)[1]);
                            temp.H2OAnlyParam_T_B = float.Parse(((string[])kvp.Value)[2]);
                            retdict.Add(newkey, temp);
                            break;
                        }
                    //float[5]
                    case 11:
                        {
                            Define.GasFixParameters temp = new Define.GasFixParameters();
                            temp.k = float.Parse(((string[])kvp.Value)[0]);
                            temp.mi = float.Parse(((string[])kvp.Value)[1]);
                            temp.ni = float.Parse(((string[])kvp.Value)[2]);
                            temp.areaMin = float.Parse(((string[])kvp.Value)[3]);
                            temp.areaMax = float.Parse(((string[])kvp.Value)[4]);
                            retdict.Add(newkey , temp);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            return retdict;
        }
    }
}


#region  解包时处理可用
//if(157 == unit)
//{

//}
//else
//    if(158 == unit)
//    {

//    }
//    else
//        if(159 == unit)
//        {

//        }
//        else
//            if(160 == unit)
//            {

//            }
//            else
//                if (161 == unit)
//                {

//                }
//                else
//                    if(162 == unit)
//                    {

//                    }
//                    else
//                        MessageBox.Show("混合类型处理超出可行范围");
#endregion
