using CensusApp.Api.Core.Domain.Model;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace CensusApp.Api.Core.Infra.Data.MongoDb.Mappings
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
                map.MapMember(x => x.Regiao).SetIsRequired(true);
                map.MapMember(x => x.IdPai).SetIsRequired(true);
                map.MapMember(x => x.IdMae).SetIsRequired(true);

            });
        }
    }
}
