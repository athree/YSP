using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCAG.Diagnosis
{
    /// <summary>
    /// 告警类型
    /// </summary>
    public enum AlarmType
    {
        Content,                //含量超标      
        Absolute,               //绝对产期速率超标
        Relative                //相对产期速率超标
    }

    public class AlarmMsg
    {
        private byte _deviceID;
        private string _gasName;
        private AlarmType _type;
        private decimal _alarmValue;        //设定的告警值
        private decimal _realValue;         //实际测量值
        private string _message;

        public byte DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        public string GasName
        {
            get { return _gasName; }
            set { _gasName = value; }
        }

        public AlarmType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public decimal AlarmValue
        {
            get { return _alarmValue; }
            set { _alarmValue = value; }
        }

        public decimal RealValue
        {
            get { return _realValue; }
            set { _realValue = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
