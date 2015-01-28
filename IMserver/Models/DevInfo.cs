namespace IMserver.Models
{
    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson;
    using MongoDB.Driver;

    //[BsonDiscriminator("devinfo")]
    //[BsonKnownTypes(typeof(SIML))]
   
    public class DevInfo
    {
        [ScaffoldColumn(false)]
        public ObjectId _id;

        [Key,Required, Display(Name = "编号")]
        public string DevID { get; set; }

        [Display(Name="设备名称")]
        public string DevName { get; set; }

        [Required, Display(Name = "类型")]
        public string Type { get; set; }

        [Required,Display(Name="单位名称")]
        public string CompName { get; set; }

        [Required, Display(Name = "GPRS号")]
        public string GPRSID { get; set; }

        [Display(Name = "添加时间")]
        public Nullable<System.DateTime> AddTime { get; set; }

        [BsonIgnoreIfNull]
        public Nullable<System.DateTime> DeleteTime { get; set; }
           
       
    }
}
