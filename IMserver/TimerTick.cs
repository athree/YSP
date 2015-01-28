using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using IMserver.CommonFuncs;

namespace IMserver
{
    /// 定时器时间调用的方法的格式
    ///  public void test1(object source, System.Timers.ElapsedEventArgs e){}
    public class TimerTick
    {
        private System.Timers.Timer loopevent;    //定时器
        public int counter;                       //提供定时方法的时基计数
        public int cnt;                           //定义计时门限
        //超出30秒清除发送后等待队列
        public static int count = 180;            //相应的消息已经存在的时间标签

        /// <summary>
        /// 构造函数重载 1，配合下面的loopadd和timeout方法判断是否超时
        /// </summary>
        /// <param name="count">超时门限</param>
        public TimerTick()
        {
            //counter = 0;
            //cnt = count;                              //初始化定时器                          
            loopevent = new System.Timers.Timer(2500);  //指定10毫秒的时基单位
            loopevent.Elapsed += new ElapsedEventHandler(TimeOut);
            loopevent.AutoReset = true;
        }

        /// <summary>
        /// 构造函数重载2，定时执行指定方法（需自己写方法指定，注意方法参数格式）
        /// </summary>
        /// <param name="count">定时门限</param>
        /// <param name="hello">定时执行方法</param>
        public TimerTick(int count, Action<Object, System.Timers.ElapsedEventArgs> e)
        {
            //create a (1ms * count) timer to loop a event
            loopevent = new System.Timers.Timer(count);      
            loopevent.Elapsed += new System.Timers.ElapsedEventHandler(e);
            loopevent.AutoReset = true;
            //this flag can be replaced by start and stop 
            //start and stop work as changing the enable
        }

        /// <summary>
        /// 计算对1970-1-1的时间差的秒数
        /// </summary>
        /// <param name="endtime">参考时间</param>
        /// <returns>间隔的秒数</returns>
        public static long TimeSpanToSecond(DateTime end)
        {
            string timestr = "1970-1-1 08:00:00";
            DateTime start = Convert.ToDateTime(timestr);
            TimeSpan span = end - start;
            return (long)span.TotalSeconds;
        }

        /// <summary>
        /// 返回日期
        /// </summary>
        /// <param name="seconds">对1970-1-1的秒数的差值</param>
        /// <returns>转换为的时间</returns>
        public static DateTime TimeSpanToDate(long seconds)
        {
            string timestr = "1970-1-1 08:00:00";
            DateTime starttime = Convert.ToDateTime(timestr);
            return starttime.AddSeconds((double)seconds); 
        }

        /// <summary>
        /// 计数器加值
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void LoopAdd(object source, System.Timers.ElapsedEventArgs e)
        {
            counter++;
        }

        /// <summary>
        /// 判断是否到达定时时间
        /// </summary>
        /// <returns> 到达：return true 没到：return false</returns>
        public bool TimeOut()
        {
            if (counter >= cnt)
            {
                counter = 0;
                return true;
            }
            else
                return false;
        }

        public void TimeOut(object source, System.Timers.ElapsedEventArgs e)
        {
            loopevent.Stop();
            //MessageBox.Show("规定的时间内没有接收到返回帧！");
            
        }

        /// <summary>
        /// 开定时器
        /// </summary>
        public void StartTimer()
        {
            loopevent.Start();
        }

        /// <summary>
        /// 暂停定时器
        /// </summary>
        public void StopTimer()
        {
            loopevent.Stop();
        }

        /// <summary>
        /// 回收定时器所占用的资源，dispose是否需要调用
        /// </summary>
        public void Close()
        {
            loopevent.Stop();    //调用close方法，直接暂停并关闭回收
            loopevent.Close();   //回收定时器占用的资源
            loopevent.Dispose();  //回收组件占用的资源
        }

        //定时更新计时字典
        public static void UpdateTimer(object source, System.Timers.ElapsedEventArgs e)
        {
            //顺序遍历存在问题，要foreach扫描
            //for (int i = 0; i < Define.existime.Count; i++)
            //{
            //    //如果超时还没有返回响应，那么清楚此项
            //    if (0 == Define.existime.ElementAt(i).Value)
            //    {
            //        Define.existime.Remove(Define.existime.ElementAt(i).Key);
            //    }
            //    //如果没有超时则继续等待，同时计时减
            //    else
            //    {
            //        Define.existime[Define.existime.ElementAt(i).Key]--;
            //    }
            //}
            //集合已经改变无法枚举遍历
            //foreach (KeyValuePair<byte, int> kvp in Define.existime)
            //{
            //    if (0 == kvp.Value)
            //    {
            //        Define.existime.Remove(kvp.Key);
            //    }
            //    else
            //    {
            //        Define.existime[kvp.Key]--;
            //    }
            //}
            Dictionary<byte, int>.KeyCollection dict_kc = Define.existime.Keys;
            List<byte> removecache = new List<byte>();
            List<byte> updatecache = new List<byte>();
            //foreach (byte num in dict_kc)
            //不论是修改集合中的一部分还是所有都无法在以枚举类型遍历集合
            foreach (KeyValuePair<byte, int> kvp in Define.existime)
            {
                if (0 == kvp.Value)
                {
                    removecache.Add(kvp.Key);
                }
                else
                {
                    updatecache.Add(kvp.Key);
                }
            }
            //删除超时队列
            for (int i = 0; i < removecache.Count; i++)
            {
                Define.existime.Remove(removecache[i]);
            }
            //更新计时队列
            for (int i = 0; i < updatecache.Count; i++)
            {
                Define.existime[updatecache[i]]--;
            }
        }
    }
}
