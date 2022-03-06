using CensusApp.Api.Core.Domain;
using MongoDB.Bson.Serialization;

namespace CensusApp.Api.Core.Infra.Data.MongoDb.Mappings
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
                map.MapMember(x => x.AlteradoEm)
                .SetIgnoreIfNull(true);
                map.MapMember(x => x.ExcluidoEm)
                .SetIgnoreIfNull(true);

            });
        }
    }
}
