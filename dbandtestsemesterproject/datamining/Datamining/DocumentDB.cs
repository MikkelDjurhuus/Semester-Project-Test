using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Datamining
{
    public static class DocumentDB
    {
        static IMongoClient client = null;
        static IMongoDatabase database = null;

        public static IMongoDatabase GetConnection()
        {
            if(database == null)
            {
                client = new MongoClient();
                database = client.GetDatabase("Guttenberg");
            }
            return database;
        }
    }
}
