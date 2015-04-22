using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMserver
{
    public class MSGEncoding
    {
        //public static int _tempInt;

        #region 消息类型枚举
        public enum MsgType
        {
            LoopBack = 0x00,       //通信测试
            ReadUnit = 0x01,       //读操作单元
            WriteUnit = 0x02,      //写操作单元
            GetFileInfo = 0x03,    //文件信息查询
            ReadFile = 0x04,       //读文件
            WriteFile = 0x05,      //写文件到设备
            ReadBuffer = 0x06,     //读缓存
            Other = 0xA0    //其他（向设备查询Error Code的详细解释）
        }
        #endregion

        #region  SubMsgType消息子类型枚举
        public enum ReadUint
        {
            GetDevStatus = 0x00,       //查询设备状态
            Readcfg,                   //读取配置信息
            ReadData                   //读数据
        }

        public enum WriteUint
        {
            ControlDev = 0x00,          //控制设备
            Setcfg,                     //设置配置信息
            WriteData                   //写数据
        }

        public enum GetFileInfo
        {
            GetFileByRange = 0x00,      //按日期范围查询数据文件名及文件大小
            GetXFileByLately,           //查询离指定日期最近的x个数据文件名及文件大小
            GetcfgFile                  //取得配置文件名称(=最新更新时间)及文件大小
        }

        public enum ReadFile
        {
            ReadDataFile = 0x00,        //读取指定数据文件，从x位置开始的n个字节数据
            ReadcfgFile                 //读取指定配置文件，从x位置开始的n个字节数据
        }

        public enum WriteFile
        {
            UpDataToDev = 0x00,         //上传数据文件到设备
            UpcfgToDev,                 //上传配置文件到设备，并作为最新配置
            UpGradeFile                 //上传升级文件
        }

        public enum ReadBuffer
        {
            ReadGasCache = 0x00,        //读六种气体的采样缓存
            ReadCO2Cache,               //读CO2气体的采样缓存
            ReadH2OCache                //读H2O采样数据缓存
        }

        public enum Other
        {
            GetErrorInfo = 0x00,         //向设备查询Error Code的详细解释
            GetAlarmInfo = 0x01,         //下位机自动上送的报警信息
        }
#endregion

        #region  源设备号
        public static byte srcID = 0x00;
        #endregion

        #region 消息方向
        public enum MsgDir
        {
            Request = 0x00,             //请求
            Response                    //响应
        }
        #endregion

        #region   错误代码 (帧外数据CRC校验错误返回==44)
        public enum LastError
        {
            Success = 0,        //函数调用成功
            DBFailure,          //数据库设置失败
            DeviceFailure,      //下位机设置失败
            Timeout,            //服务器响应超时
             
            SrcIDError,         //源设备号错误
            DestIDError,        //目标设备号错误
            InvalidMsgVer,      //错误的消息版本号

            MsgDirError,        //消息方向错误
            MsgTypeError,       //消息类型错误
            MsgSubTypeError,    //子消息类型错误

            InvalidCRC,         //CRC校验失败
            InvalidPackage,     //无效的数据包
           // FrameFlagError      帧头校验失败   等同于上面的错误
        }
        #endregion

        #region  消息类型及内容版本号
        public static byte msgVer = 0x01;
        #endregion

    }
}


#region  未修改错误代码
/// <summary>
/// 错误代码
/// </summary>
//public enum LastError
//{
//    Success = 0,        //函数调用成功
//    DBFailure,          //数据库设置失败
//    DeviceFailure,      //下位机设置失败
//    Timeout,            //服务器响应超时
//    SrcIDError,         //源设备号错误
//    DestIDError,        //目标设备号错误
//    InvalidMsgVer,      //错误的消息版本号
//    MsgDirError,        //消息方向错误
//    MsgTypeError,       //消息类型错误
//    MsgSubTypeError,    //子消息类型错误
//    InvalidCRC,         //CRC校验失败
//    InvalidPackage      //无效的数据包
//}
#endregion

#region   字符串数组形式组织错误代码
//public static string[] LastErrorStr = 
//{ 
//  "设置成功", 
//  "数据库设置失败", 
//  "下位机设置失败", 
//  "服务器响应超时",
//  "源设备号错误",
//  "目标设备号错误",
//  "错误的消息版本号",
//  "消息方向错误",
//  "消息类型错误",
//  "子消息类型错误",
//  "CRC校验失败",
//  "无效的数据包"
//};
#endregion