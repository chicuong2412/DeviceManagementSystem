using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Devices.Domain.Models
{
    public class Device
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        public string DeviceCode { get; set; }

        [BsonElement("Name")]
        public string DeviceName { get; set; }

        public string Location { get; set; }

        public string? Description { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string FactoryId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdateDate { get; set; }
    }
}
