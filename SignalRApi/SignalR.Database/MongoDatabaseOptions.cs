using System;
namespace SignalR.Database
{
    public class MongoDatabaseOptions
    {
        public static string CONFIGPATH = "MongoDatabase";
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;
        public string ChatCollectionName { get; set; } = null!;
    }
}

