using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

/*
 * 使用方法1：             
            ILog log = new FileLog();
            log.Write(ex.Message + "\r\n" + ex.StackTrace);
            log.Dispose();
 * 
 * 使用方法2：             
            using (ILog log = new FileLog())
            {
                log.Write("Exception from" + ex.Source);
                log.Write("Exception:" + ex.Message);
                log.Write(ex.StackTrace);
                log.Write("----------------------------------------------------------");
            }
 */

namespace SysLog
{
    public class FileLog : ILog
    {
        private string _filePath;
        private StreamWriter _sw;

        public FileLog()
        {
            string strTime = DateTime.Now.ToString("yyyy-MM-dd");

            string modulePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string appDirectory = System.IO.Path.GetDirectoryName(modulePath) + "\\Log\\";

            _filePath = appDirectory + strTime + ".log";

            int first = 0;
            while (_filePath.IndexOf("\\", first) >= 0)                  //创建完整目录
            {
                string str = _filePath.Substring(0, _filePath.IndexOf("\\", first));
                if (!System.IO.Directory.Exists(str))
                {
                    System.IO.Directory.CreateDirectory(str);
                }
                first = _filePath.IndexOf("\\", first) + 1;
            }

            _sw = new StreamWriter(_filePath, true, Encoding.Default);
        }

        public void Write(string str)
        {
            _sw.WriteLine(str);
        }

        public void Dispose()
        {
            _sw.Close();
        }

    }
}
