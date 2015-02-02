using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMserver.Models
{
    //接口的实现
    public class IIGenerator
    {
        private static int id = 0;
        object GenerateId(object container, object document)
        {
            return id++;
        }

        bool IsEmpty(object id)
        {
            return (0 == (int)id) ? true : false;
        }
    }
}
