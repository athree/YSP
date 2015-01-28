using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace IMserver.Models
{
    [BsonDiscriminator("devtype")]
    public class DevType
    {
        [ScaffoldColumn(false)]
        [BsonId]
        public ObjectId TypeId{get;set;}

        [Display(Name = "类型名称")]
        public string TypeName { get; set; }

    }
}