using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace christmas_eve.Classes
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
        
        [BsonIgnoreIfNull]
        [BsonElement("password_clear_text")]
        public string Password_Clear { get; set; }

        [BsonElement("screenname")]
        public string ScreenName { get; set; }

        [BsonElement("isAdmin")]
        public Boolean isAdmin { get; set; }

    }
}
