using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HydraPeak.Web.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace HydraPeak.Web.DataAccess.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private const string MongoEnvironmentVarConnectionStringKey = "MongoDbConnectionString";
        private readonly IMongoClient client;
        private readonly MongoCollectionSettings settings;
        private readonly string databaseName;

        public GenericRepository(IConfiguration configuration, IMongoClient client)
        {
            // If we have an environment variable configured for connection string, use that instead.
            var environmentConnectionString = Environment.GetEnvironmentVariable(MongoEnvironmentVarConnectionStringKey);
            
            if (!string.IsNullOrWhiteSpace(environmentConnectionString))
            {
                this.client = new MongoClient(environmentConnectionString);
                // This could be wrong, but there's no way to find the value from the client :/
                this.databaseName = configuration.GetSection("MongoDb:Database").Value;
            }
            else
            {
                this.client = client;
                this.databaseName = configuration.GetSection("MongoDb:Database").Value;
            }

            // TODO: recommended by MLab, but settings are frozen :/
            //client.Settings.RetryWrites = true;

            this.settings = new MongoCollectionSettings()
            {
                AssignIdOnInsert = true,
                WriteConcern = WriteConcern.WMajority, // Required by MLab
            };
        }

        public T SingleOrDefault<T>(Expression<Func<T, bool>> predicate)
        {
            var collection = this.GetCollection<T>();
            return collection.Find(predicate).SingleOrDefault();
        }

        public void Insert<T>(T obj)
        {
            var collection = this.GetCollection<T>();
            collection.InsertOne(obj);
        }

        public IEnumerable<T> All<T>()
        {
            var collection = this.GetCollection<T>();
            // TL;DR we're probably consuming lots of memory here returning all, but we need it.
            // Returning AsQueryable causes this weird case; update any field (e.g. gold = 1) returns,
            // save user executes, and gold is back to 0; probably because it's re-evaluated
            // from Mongo directly instead of preserving the in-memory changes thus far.
            return collection.AsQueryable().ToList();
        }

        public void Update<T>(T target) where T : HasId
        {
            // Kinda sorta like assumes target has an ObjectId field named Id. Also assumes that reflection
            // is sufficiently fast given how often we're doing this. Profile later if there's a problem.
            var collection = this.GetCollection<T>();
            var filter = Builders<T>.Filter.Eq("Id", target.Id);
            collection.ReplaceOne(filter, target, new ReplaceOptions());
        }

        private IMongoCollection<T> GetCollection<T>()
        {
            var nameParts = typeof(T).Name.Split('.');
            var repositoryName = nameParts[nameParts.Length - 1];
            var collection = this.client.GetDatabase(this.databaseName).GetCollection<T>(repositoryName, this.settings);
            return collection;
        }
    }
}