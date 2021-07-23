using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesslayer.Models;
using MongoDB.Driver;

namespace DataAccesslayer
{
    public class MongoConnectionHelper
    {


        private readonly IMongoDatabase mongoDb;
        public MongoConnectionHelper()
        {

        mongoDb= new MongoClient(ConfigurationManager.AppSettings["connectionString"]).GetDatabase("Todo");
        
        }

        public IMongoDatabase GetConnection()
        {
            return mongoDb;
        }

    }
}
