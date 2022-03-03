using CensusApp.Api.Core.Domain;
using MongoDB.Bson.Serialization;

namespace CensusApp.Api.Core.Infra.Data.MongoDb.Maps
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
