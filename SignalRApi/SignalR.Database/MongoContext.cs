using System;
using Amazon.Runtime.Internal.Util;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SignalR.Database.Models;

namespace SignalR.Database
{
    public abstract class MongoContext<T> where T : class
    {
        public readonly MongoClient mongoClient = null!;
        public readonly IMongoDatabase mongoDatabase = null!;

        public readonly ILogger<T>? _logger;
        public MongoContext(IOptions<MongoDatabaseOptions> chatDatabaseOptions, ILogger<T>? logger)
        {
            mongoClient = new MongoClient(chatDatabaseOptions.Value.ConnectionString);
            mongoDatabase = mongoClient.GetDatabase(chatDatabaseOptions.Value.DatabaseName);
            _logger = logger;
        }
        public IMongoCollection<C>? CreateOrGetCollection<C>(string collectionName) where C : class
        {
            try
            {
                if (!mongoDatabase.ListCollectionNames().ToList().Contains(collectionName))
                {
                    mongoDatabase.CreateCollection(collectionName);
                }
                return mongoDatabase.GetCollection<C>(collectionName);
            }
            catch (Exception ex)
            {
                _logger?.LogError("Unable to create collection {0} exception {1} ", collectionName, ex);
            }
            return null;
        }
    }
}

