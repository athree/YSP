using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using IMserver.Models;
using IMserver.Models.SimlDefine;

namespace IMserver.DBservice
{
    public class YSPservice
    {
        public Config GetCFG(string devId)
        {
            try
            {
                MongoHelper<Config> _cfg = new MongoHelper<Config>();
                Expression<Func<Config, bool>> ex = p => p.DevID == devId;
                Config cfg = _cfg.FindOneBy(ex);
                return cfg;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
        public StateCtrol GetSC(string devId)
        {
            try
            {
                MongoHelper<SIML> _siml = new MongoHelper<SIML>();
                Expression<Func<SIML, bool>> ex = p =>(p.DevID == devId && p.SC!=null);
                StateCtrol mySC = _siml.FindOneBy(ex).SC;
                return mySC;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}