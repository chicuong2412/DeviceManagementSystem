using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devices.Domain.Data;
using Devices.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Devices.Domain.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
    {

        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<TEntity> _collection;

        public BaseRepository(IOptions<DatabaseSettings> options)
        {
            var clientSettings = MongoClientSettings.FromConnectionString(options.Value.ConnectionString);
            var mongoClient = new MongoClient(clientSettings);

            _database = mongoClient.GetDatabase(options.Value.DatabaseName);
            _collection = _database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task<TEntity> Add(TEntity obj)
        {
            await _collection.InsertOneAsync(obj);
            return obj;
        }

        public async Task<List<TEntity>> AddMany(List<TEntity> objs)
        {
            await _collection.InsertManyAsync(objs);
            return objs;
        }

        public async Task<TEntity> AddOrUpdate(string id, TEntity obj)
        {
            await _collection.ReplaceOneAsync(FilterId(id), obj, new ReplaceOptions { IsUpsert = true });
            var data = await _collection.Find(FilterId(id)).SingleOrDefaultAsync();
            return data;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public Task<TEntity> GetById(string id)
        {

            return _collection.Find(Builders<TEntity>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> Read()
        {
            var mongoCollection = _database.GetCollection<TEntity>(typeof(TEntity).Name);
            return mongoCollection.AsQueryable();
        }

        public IMongoCollection<TEntity> ReadCollection()
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task<bool> Remove(string id)
        {
            var data = await _collection.DeleteOneAsync(FilterId(id));
            return data.IsAcknowledged;
        }

        public async Task<TEntity> Update(string id, TEntity obj)
        {
            await _collection.ReplaceOneAsync(FilterId(id), obj);
            var data = await _collection.Find(FilterId(id)).SingleOrDefaultAsync();
            return data;
        }

        public static FilterDefinition<TEntity> FilterId(string key)
        {
            return Builders<TEntity>.Filter.Eq("_id", new ObjectId(key));
        }

        public static FilterDefinition<TEntity> Filter<TValue>(string key, TValue value)
        {
            return Builders<TEntity>.Filter.Eq(key, value);
        }
    }
}
