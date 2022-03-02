using CensusApp.Api.Core.Domain._Base;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Infra.Data
{
    public class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly IMongoCollection<TEntity> _collection;
        public MongoDbRepository(IMongoDatabase mongoDatabase)
        {
            _collection = mongoDatabase.GetCollection<TEntity>(GetCollectionName());
        }
        public void Add(TEntity obj)
        {
            _collection.InsertOne(obj);
        }
        public Task AddAsync(TEntity obj)
        {
            return _collection.InsertOneAsync(obj);
        }
        public void Delete(TEntity obj)
        {
            obj.Delete();
            Update(obj);
        }
        public void Update(TEntity obj)
        {
            _collection.ReplaceOne(FilterById(obj.Id), obj);
        }
        public Task UpdateAsync(TEntity obj)
        {
            return _collection.ReplaceOneAsync(FilterById(obj.Id), obj);
        }
        public TEntity Get(object id)
        {
            return _collection.Find(FilterById(id)).FirstOrDefault();
        }
        private string GetCollectionName()
        {
            return ToString().GetType().Name;
        }
        private FilterDefinition<TEntity> FilterById(object id)
        {
            return Builders<TEntity>.Filter.Eq(doc => doc.Id, id);
        }
    }
}
