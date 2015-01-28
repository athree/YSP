using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMserver.CommonFuncs
{
    public class L_MBS
    {
        /// <summary>
        /// 1、数据的大小端转化
        /// 2、双向转化，输入/出循环
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ushort ConvertBS(ushort data)
        {
            byte low = (byte)data;
            byte high = (byte)(data >> 8);

            return (ushort)(low << 8 | high);
        }

        public static float ConvertBS(float f)
        {
            byte[] temp = ByteStruct.StructToBytes(f);
            Array.Reverse(temp);
            return (float)ByteStruct.BytesToStruct(temp, typeof(float));
        }

        public static int ConvertBS_int(int i)
        {
            byte[] temp = ByteStruct.StructToBytes(i);
            Array.Reverse(temp);
            return (int)ByteStruct.BytesToStruct(temp, typeof(int));
        }
    }
}
