using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication1.Diagnosis
{
    /// <summary>
    /// 点坐标的定义
    /// </summary>
    public class Point
    {
        public decimal X;
        public decimal Y;
        public int index;   //点在序列中的位置
    }

    /// <summary>
    /// 关键点定义，关键点是每组气体出峰区间的左边最低点、最高点和右边最低点
    /// </summary>
    public class KeyPoint
    {
        public Point Left;
        public Point Right;
        public Point High;
    }

    /// <summary>
    /// 每组气体的出峰区间
    /// </summary>
    public class Range
    {
        public int Start;
        public int End;
    }

    




}
