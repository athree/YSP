using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using IMserver.CommonFuncs;
using System.Collections;

namespace IMserver.SubFuncs
{
    public class WriteFile
    {
        //文件类型的枚举
        public enum FileType
        {
            CfgOrDataFile = 0,
            UpGradeFile = 1,
        }

        public struct Parameter
        {
            //在索引fileitem时代替filename，默认编码能一一对应文件名
            //public byte packetnum;
            public long filename;
            public FileStream filehandle;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] upgradefilename;
            ///组帧中不掺杂文件的读取，即不传入文件的读取起点和终点，
            ///在packetmsg外截取好文件内容传入byte[] filedata
            public ushort start;
            //本次请求读的字节数
            public ushort writebytes;
            //文件的大小
            public ushort filesize;
            //本次返回实际读的字节数
            //public ushort doneread;
        }

        //升级文件目前认为是字符串转字节数组，可以比16小，这里只看有效长度而发送时对其16
        //用到这个类键-值加入到hashtable说明是要传送的文件

        public class FileItem
        {
            ushort filesize;
            bool isupgradefile;
            ushort index;
            public long filename;
            public FileStream filehandle;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] upgradefilename;
            public bool IsUpgradeFile { get { return isupgradefile; }}
            public ushort FileIndex { get { return index; } set { index = value; } }
            public ushort FileSize { get { return filesize; }}
            public FileItem(long fn, byte[]ufn, FileType ft, ushort fz)
            {
                //配置或者数据文件
                if (FileType.CfgOrDataFile == ft)
                {
                    isupgradefile = false;
                    string path = "d:\\"+fn.ToString()+".txt";
                    filename = fn;
                    filesize = fz;
                    filehandle = new FileStream(path , FileMode.Open);
                }
                //升级文件
                else
                {
                    isupgradefile = true;
                    string path = "d:\\"+Encoding.ASCII.GetString(ufn)+".txt";
                    upgradefilename = ufn;
                    filesize = fz;
                    filehandle = new FileStream(path , FileMode.Open);
                }
            }
            ~FileItem()
            {
                filehandle.Flush();
                filehandle.Close();
            }
        }

        //传送的文件的缓冲池
        //私有改共有，原打算所有操作都在类内执行，但组包调用方法还是要访问，但也可以考虑返回累加接收
        public static Hashtable filepool = new Hashtable();

        //public static ushort MAX = 256;
        public static int datacfglen = 8;
        public static int upgradelen = 16;
        /// <summary>
        /// 上传数据文件到设备
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] UpcfgToDev(byte destid , WriteFile.Parameter para)
        {
            byte[] donedata;
            //由于结构体中普通文件和升级文件的名字都会给出，通过getdata的第二参数标定，为0为普通文件（数据和配置）
            //不再讨论文件类别，只给出编号，相应的文件以包装进类中
            //恢复文件类型标准，由于para和fileitem死循环产生
            byte[] tempdata = GetData(para , (int)FileType.CfgOrDataFile);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.WriteFile;
            _frame.msgSubType = (byte)MSGEncoding.WriteFile.UpcfgToDev;
            _frame.dataLen = (ushort)tempdata.Length;
            donedata = PrepareData.MergeMsg(ref _frame, tempdata);
            return donedata;
        }

        /// <summary>
        /// 上传配置文件到设备，并作为最新配置
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] UpDataToDev(byte destid, WriteFile.Parameter para)
        {
            byte[] donedata;
            byte[] tempdata = GetData(para ,(int)FileType.CfgOrDataFile);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.WriteFile;
            _frame.msgSubType = (byte)MSGEncoding.WriteFile.UpDataToDev;
            _frame.dataLen = (ushort)tempdata.Length;
            donedata = PrepareData.MergeMsg(ref _frame, tempdata);
            return donedata;
        }

        /// <summary>
        /// 上传升级文件到设备
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] UpGradeFile(byte destid, WriteFile.Parameter para)
        {
            byte[] donedata;
            byte[] tempdata = GetData(para , (int)FileType.UpGradeFile);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.WriteFile;
            _frame.msgSubType = (byte)MSGEncoding.WriteFile.UpGradeFile;
            _frame.dataLen = (ushort)tempdata.Length;
            donedata = PrepareData.MergeMsg(ref _frame, tempdata);
            return donedata;
        }

        /// <summary>
        /// 获取数据域,依赖于上次读到的总和为文件开始读取位置，如果读取到数据则说明未读完，继续组文件发送包
        /// 如果读到字节数为0，那么说明读取完毕，将本次读到的字节数ref返回，在外判断循环组包结束
        /// </summary>
        /// <returns></returns>
        public static byte[] GetData(WriteFile.Parameter para , int type)
        {
            byte[] ret;
            //读请求个数的字节
            byte[] filedata = new byte[para.writebytes];
            //这里读完到之后指针已偏移，默认指定读到的数据能全部发送
            para.filehandle.Seek(para.start , SeekOrigin.Begin);
            ushort writedown = (ushort)para.filehandle.Read(filedata, 0, para.writebytes);
            //((FileItem)filepool[para.packetnum]).FileIndex += writedown;
            if (0 != writedown)
            {
                if ((int)FileType.UpGradeFile == type)
                {
                    ret = new byte[writedown + upgradelen];
                    Array.Copy(para.upgradefilename, ret, upgradelen);
                    Array.Copy(filedata, 0, ret, upgradelen, writedown);
                }
                else
                {
                    ret = new byte[writedown + datacfglen];
                    byte[] temp = ByteStruct.StructToBytes(para.filename);
                    Array.Copy(temp, ret, datacfglen);
                    Array.Copy(filedata, 0, ret, datacfglen, writedown);
                }
            }
            //当读到0个字节，即已经读到文件结尾的时候，直接返回文件名，为结束帧
            else
            {
                if (1 == type)
                {
                    ret = para.upgradefilename;
                }
                else
                {
                    ret = ByteStruct.StructToBytes(para.filename);
                }
            }
            return ret;
        }

        /// <summary>
        /// 接收到消息的处理,返回解析响应中下位机写入的字节数，用于外边判断是否继续传输（判断报错）
        /// </summary>
        /// <returns></returns>
        public static ushort MsgHandle(byte[] b_recvframe, ref ushort errorcode)
        {
            ushort thisdown = 0;
            //返回帧情况:如果为数据或配置文件只能是短暂；如果是升级文件，只能是长帧（文件名就16字节）
            //注意：这里统一数据配置和升级文件的交互方法，响应包应该全是短包
            //FileItem fi = (FileItem)filepool[hdtr.packetnum];
            PrepareData.Msg_Bus temp = (PrepareData.Msg_Bus)ByteStruct.BytesToStruct(b_recvframe,
                                                                                        typeof(PrepareData.Msg_Bus));
            //包含执行状态字
            byte[] rawdata = temp.data;
            ushort datalen = (ushort)(temp.dataLen - 2);
            //终端没有接收到字节: 出错或者是文件传送完成握手响应
            if (0 == datalen)
            {
                byte[] _errorcode = new byte[2];
                Array.Copy(rawdata, _errorcode, 2);
                errorcode = BitConverter.ToUInt16(_errorcode, 0);
                thisdown = 0;
            }
            //终端接收到一定的数据
            else
            {
                byte[] _errorcode = new byte[2];
                byte[] _thisdonw = new byte[2];
                Array.Copy(rawdata, _errorcode, 2);
                Array.Copy(rawdata, 2, _thisdonw, 0, 2);
                errorcode = BitConverter.ToUInt16(_errorcode, 0);
                thisdown = BitConverter.ToUInt16(_thisdonw, 0);
            }
            return thisdown;
            #region  区别数据、配置文件和升级文件
            //如果为数据文件或者配置文件
            /*if (!fi.IsUpgradeFile)
            {
                PrepareData.Msg_Bus temp = (PrepareData.Msg_Bus)ByteStruct.BytesToStruct(b_recvframe,
                                                                                         typeof(PrepareData.Msg_Bus));
                //包含执行状态字
                byte[] rawdata = temp.data;
                ushort datalen = (ushort)(temp.dataLen - 2);
                //终端没有接收到字节: 出错或者是文件传送完成握手响应
                if (0 == datalen)
                {
                    byte[] _errorcode = new byte[2];
                    Array.Copy(rawdata, _errorcode, 2);
                    errorcode = BitConverter.ToUInt16(_errorcode, 0);
                    thisdown = 0;
                }
                //终端接收到一定的数据
                else
                {
                    byte[] _errorcode = new byte[2];
                    byte[] _thisdonw = new byte[2];
                    Array.Copy(rawdata, _errorcode, 2);
                    Array.Copy(rawdata, 2, _thisdonw, 0, 2);
                    errorcode = BitConverter.ToUInt16(_errorcode, 0);
                    thisdown = BitConverter.ToUInt16(_thisdonw, 0);
                }
            }
            //如果为升级文件
            else
            {
                byte 数据分割
            }
             * */
#endregion
            //错误代码      数据域        含义
            //   0            0         服务器发送文件写结束帧后下位机的正常响应
            //   0          ！= 0       正常响应下位机接收到服务器的文件数据并写入的正常响应
            // ！= 0         null       有错误发生时的响应帧
        }
    }
}
