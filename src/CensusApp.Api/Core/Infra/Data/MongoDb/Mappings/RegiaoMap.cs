using CensusApp.Api.Core.Domain.Model;
using MongoDB.Bson.Serialization;

namespace CensusApp.Api.Core.Infra.Data.MongoDb.Mappings
{
    public class RegiaoMap : IMongoDbClassMap
    {
        public void CreateMap()
        {
            BsonClassMap.RegisterClassMap<Regiao>(map =>
            {
                map.SetIgnoreExtraElements(true);
                map.MapMember(x => x.Descricao);
            });
        }
    }
}
