using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace christmas_eve.Classes
{
    public class MongoDB : IDataBase
    {
        private IMongoDatabase database
        {
            get
            {
                return MongoConnection.Instance.Database;
            }
        }

        public Users  GetUser(Users user)
        {
            IMongoCollection<Users> userCollection = database.GetCollection<Users>("users");
            return userCollection.Find(_ => _.Email == user.Email && _.Password_Clear == user.Password_Clear).FirstOrDefault();
        }

        public Toys GetToy(string id)
        {
            IMongoCollection<Toys> toyCollection = database.GetCollection<Toys>("toys");
            return toyCollection.Find(_ => _.ID == id).FirstOrDefault();
        }

        public IEnumerable<Toys> GetAllToys()
        {
            IMongoCollection<Toys> toyCollection = database.GetCollection<Toys>("toys");
            return toyCollection.Find(new BsonDocument()).ToList();
        }

        public IEnumerable<Orders> GetAllOrder()
        {
            IMongoCollection<Orders> requestCollection = database.GetCollection<Orders>("orders");
            return requestCollection.Find(new BsonDocument()).SortBy(t => t.Date).ToList();
        }

        public Orders GetOrder(string id)
        {
            IMongoCollection<Orders> requestCollection = database.GetCollection<Orders>("orders");
            return requestCollection.Find(_ => _.ID == id).FirstOrDefault();
        }

        public bool UpdateStatus(Orders requestKid)
        {
            IMongoCollection<Orders> requestCollection = database.GetCollection<Orders>("orders");
            var filter = Builders<Orders>.Filter.Eq("_id", ObjectId.Parse(requestKid.ID));
            var update = Builders<Orders>.Update
                .Set("status", requestKid.Status);
            try
            {
                requestCollection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateAmountToy(Toys toy)
        {
            IMongoCollection<Toys> toyCollection = database.GetCollection<Toys>("toys");
            var filter = Builders<Toys>.Filter.Eq("_id", ObjectId.Parse(toy.ID));
            var update = Builders<Toys>.Update
                .Inc("amount", -1);

            try
            {
                toyCollection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveToy(string id)
        {
            IMongoCollection<Toys> toyCollection = database.GetCollection<Toys>("toys");
            var filter = Builders<Toys>.Filter.Eq("_id", ObjectId.Parse(id));


            try
            {
                toyCollection.DeleteOne(filter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Decimal> SumRequest(string name)
        {
            IMongoCollection<Toys> toyCollection = database.GetCollection<Toys>("toys");
            var match = new BsonDocument
                {
                    {
                        "$match",
                        new BsonDocument
                            {
                                {"Name", name}
                            }
                    }
                };


            var group = new BsonDocument
                {
                    { "$group",
                        new BsonDocument
                            {
                                { "_id", new BsonDocument
                                             {
                                                 {
                                                     "MyUser","$User"
                                                 }
                                             }
                                },
                                {
                                    "Count", new BsonDocument
                                                 {
                                                     {
                                                         "$sum", "$Count"
                                                     }
                                                 }
                                }
                            }
                  }
                };


            var pipeline = new[] { match, group };

            var result = toyCollection.Aggregate<Decimal>(pipeline);
            return result.ToList();



        }
    }
}
