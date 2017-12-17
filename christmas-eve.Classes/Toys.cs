using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace christmas_eve.Classes
{
    public class Toys
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("cost")]
        public decimal Price { get; set; }

        [BsonElement("amount")]
        public int Amount { get; set; }

}
}
