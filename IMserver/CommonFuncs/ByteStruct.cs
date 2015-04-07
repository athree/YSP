using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace IMserver.CommonFuncs
{
    public class ByteStruct
    {
        /// <summary>
        /// 结构体类型转换为byte字节数组
        /// 进行类型转换之前需在object和struct之间经alloc转换
        /// ！！！异常
        /// </summary>
        /// <param name="structobj">要转换的结构体</param>
        /// <returns>返回byte字节数组</returns>
        public static byte[] StructToBytes(object structobj)
        {
            try
            {
                int size = Marshal.SizeOf(structobj);                   //获取目标结构体的大小
                byte[] bytes = new byte[size];                         //申请等同大小的字节数组空间,这样申请为返回值
                IntPtr structptr = Marshal.AllocHGlobal(size);          //申请非托管状态下的等同大小的内存空间，用来做中间转换
                Marshal.StructureToPtr(structobj, structptr, false);  //将托管状态下的对象空间数据封送到非托管内存块
                Marshal.Copy(structptr, bytes, 0, size);             //将内存空间拷贝到字节数组
                Marshal.FreeHGlobal(structptr);                         //释放非托管内存空间
                return bytes;
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }


        /// <summary>
        /// byte数组转化为指定的结构体
        /// 如果超过指定的长度那么返回错误
        /// ！！！异常
        /// </summary>
        /// <param name="bytes">要转换的byte字节数组</param>
        /// <param name="type">结构体类型</param>
        /// <returns>返回转换后恶结构体</returns>
        public static object BytesToStruct(byte[] bytes , Type type)
        {
            try
            {
                if (null == bytes)
                {
                    return null;
                }
                int size = Marshal.SizeOf(type);
                if (size > bytes.Length)
                {
                    return null;
                }
                IntPtr structPtr = Marshal.AllocHGlobal(size);           //申请等同大小的非托管状态下的内存空间
                Marshal.Copy(bytes, 0, structPtr, size);                 //将byte数组拷到分配好的内存空间，只拷贝size大小的空间
                object obj = Marshal.PtrToStructure(structPtr, type);    //将内存空间转换为目标结构体
                Marshal.FreeHGlobal(structPtr);                          //释放内存空间
                return obj;                                              //返回结构体
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }
    }
}
