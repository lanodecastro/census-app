using CensusApp.Api.Core.Domain._Base;
using MongoDB.Bson.Serialization;

namespace CensusApp.Api.Core.Infra.Data.MongoDb.Maps
{
    public class EntityMap : IMongoDbClassMap
    {
        public void CreateMap()
        {
            BsonClassMap.RegisterClassMap<Entity>(map =>
            {
                map.MapIdMember(x => x.Id);
                map.SetIgnoreExtraElements(true);
                map.MapMember(x => x.CriadoEm);
                map.MapMember(x => x.AlteradoEm);
                map.MapMember(x => x.ExcluidoEm);
            });
        }
    }
}
