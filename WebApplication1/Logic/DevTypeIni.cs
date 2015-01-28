using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMserver.DBservice;
using IMserver.Models;

namespace WebApplication1.Logic
{
    public class DevTypeIni
    {
        public MongoHelper<DevType> _devType = new MongoHelper<DevType>();
        public DevTypeIni(){
            try
            { 
                if (_devType.FindAll().FirstOrDefault() == null)
                {
                    DevType myDevType = new DevType();                
                    myDevType.TypeName = "油色谱在线监测";
                    _devType.Insert(myDevType);
                    myDevType = new DevType();
                    myDevType.TypeName = "GIS局放";
                    _devType.Insert(myDevType);
                    myDevType = new DevType();
                    myDevType.TypeName = "变压器局放";
                    _devType.Insert(myDevType);
                    myDevType = new DevType();
                    myDevType.TypeName = "开关柜测温";
                    _devType.Insert(myDevType);
                    myDevType = new DevType();
                    myDevType.TypeName = "铁芯接地";
                    _devType.Insert(myDevType);
                    myDevType = new DevType();
                    myDevType.TypeName = "避雷器";
                    _devType.Insert(myDevType);
                    myDevType = new DevType();
                    myDevType.TypeName = "容性设备";
                    _devType.Insert(myDevType);
                    myDevType = new DevType();
                    myDevType.TypeName = "SF6微水密度";
                    _devType.Insert(myDevType);
                    myDevType = new DevType();
                    myDevType.TypeName = "SF6泄露";
                    _devType.Insert(myDevType);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("数据库连接出错！请检查数据库！");
                Console.WriteLine(ex.Message);               
                return;
            }
        }
        
    }
}