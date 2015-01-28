namespace IMserver.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;


    //[BsonDiscriminator(RootClass = true)]
   
    public class DevSite
    {
        
        [ ScaffoldColumn(false)]
        [BsonId]
        public ObjectId LocateID { get; set; }
      
        [Display(Name = "单位名称"),Required]
        public string CompName { get; set; }

        [Display(Name = "经度"), Range(73.33, 135.05),Required]
        public Nullable<double> Lng { get; set; }

        [Display(Name = "纬度"), Range(3.51, 53.33), Required]
        public Nullable<double> Lat { get; set; }

        [Display(Name = "详细地址"), Required]
        public string Address { get; set; }
    }
}
