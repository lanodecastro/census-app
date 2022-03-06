using CensusApp.Api.Core.Domain.Model;
using MongoDB.Bson.Serialization;

namespace CensusApp.Api.Core.Infra.Data.MongoDb.Mappings
{
    public class RacaCorMap : IMongoDbClassMap
    {
        public void CreateMap()
        {
            BsonClassMap.RegisterClassMap<RacaCor>(map =>
            {
                map.SetIgnoreExtraElements(true);
                map.MapMember(x => x.Descricao);
            });
        }
    }
}
