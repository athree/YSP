using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace IMserver.Models
{
    [BsonDiscriminator("DevID_EP")]
    public class DevID_EP
    {
        [ScaffoldColumn(false)]
        public ObjectId _id;

        [Key, Required, Display(Name = "设备号")]
        public string DevID { get; set; }
        //远端终结点的IP和端口号的string表示
        public string endpoint { get; set; }
    }
}