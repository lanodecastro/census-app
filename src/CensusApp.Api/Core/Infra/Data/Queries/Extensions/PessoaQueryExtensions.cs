using CensusApp.Api.Core.Domain.Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CensusApp.Api.Core.Infra.Data.Queries.Extensions
{
    public static class PessoaQueryExtensions
    {
        public static IMongoQueryable<Pessoa> Nome(this IMongoQueryable<Pessoa> filter, string nome)
        {
            if (!string.IsNullOrEmpty(nome))
                return filter.Where(x => x.Nome== nome);

            return filter;
        }
        public static IMongoQueryable<Pessoa> Sobrenome(this IMongoQueryable<Pessoa> filter, string sobrenome)
        {
            if (!string.IsNullOrEmpty(sobrenome))
                return filter.Where(x => x.Sobrenome==sobrenome);

            return filter;
        }
        public static IMongoQueryable<Pessoa> NomeSobrenome(this IMongoQueryable<Pessoa> filter, string nomeSobrenome)
        {
            if (string.IsNullOrEmpty(nomeSobrenome))
                return filter;

            var nomeSobrenomeSplit = nomeSobrenome.Split(' ');
            if (nomeSobrenomeSplit.Length != 2) 
                return filter;


            return filter.Where(x => x.Nome == nomeSobrenomeSplit[0] && x.Sobrenome == nomeSobrenomeSplit[1]);
           
        }
        public static IMongoQueryable<Pessoa> Escolaridade(this IMongoQueryable<Pessoa> filter, string idEscolaridade)
        {
            if (!string.IsNullOrEmpty(idEscolaridade))
                return filter.Where(x => x.Escolaridade.Id== idEscolaridade);

            return filter;
        }
        public static IMongoQueryable<Pessoa> RacaCor(this IMongoQueryable<Pessoa> filter, string idRacaCor)
        {
            if (!string.IsNullOrEmpty(idRacaCor))
                return filter.Where(x => x.RacaCor.Id== idRacaCor);

            return filter;
        }
        public static IMongoQueryable<Pessoa> Regiao(this IMongoQueryable<Pessoa> filter, string idRegiao)
        {
            if (!string.IsNullOrEmpty(idRegiao))
                return filter.Where(x => x.Regiao.Id==idRegiao);

            return filter;
        }

    }
}
