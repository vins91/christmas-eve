using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace christmas_eve.Classes
{
    public class Orders
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("status")]
        public int Status { get; set; }

        [BsonElement("kid")]
        public string KidName { get; set; }

        [BsonElement("requestDate")]
        public DateTime Date { get; set; }

        [BsonElement("toys")]
        public List<ToyName> ToyKids { get; set; }

        public List<Decimal> PriceRequest { get; set; }

    }

    public class ToyName
    {
        [BsonElement("name")]
        public string Toy { get; set; }
    }
}
