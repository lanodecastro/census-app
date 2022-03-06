using CensusApp.Api.Core.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;

namespace CensusApp.Api.Core.Infra.Data.MongoDb.Extensions
{
    public static class MongoDbExtensions
    {
        public static IMongoCollection<TDocument> GetCollection<TDocument>(this IMongoDatabase mongoDatabase)
        {
            return mongoDatabase.GetCollection<TDocument>(typeof(TDocument).Name);
        }
        public static IMongoQueryable<TDocument> AsQueryable<TDocument>(this IMongoCollection<TDocument> collection,bool ignoreDeletedItens)
        {
            if(ignoreDeletedItens)
            return collection
                .AsQueryable<TDocument>()
                .Where(x=>(x as Entity).ExcluidoEm==null);

            return collection.AsQueryable();
        }
    }
}
