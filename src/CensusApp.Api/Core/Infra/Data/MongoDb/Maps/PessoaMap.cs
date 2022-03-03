using CensusApp.Api.Core.Domain;
using MongoDB.Bson.Serialization;

namespace CensusApp.Api.Core.Infra.Data.MongoDb.Maps
{
    public class PessoaMap : IMongoDbClassMap
    {
        public void CreateMap()
        {
            BsonClassMap.RegisterClassMap<Pessoa>(map =>
            {
                map.SetIgnoreExtraElements(true);
                map.MapMember(x => x.Nome).SetIsRequired(true);
                map.MapMember(x => x.Sobrenome).SetIsRequired(false);
                map.MapMember(x => x.RacaCor).SetIsRequired(true);
                map.MapMember(x => x.Escolaridade).SetIsRequired(true);
                map.MapMember(x => x.Mae).SetIsRequired(true);
                map.MapMember(x => x.Pai).SetIsRequired(false);
            });
        }
    }
}
