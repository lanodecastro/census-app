using CensusApp.Api.Core.Domain.Commands;
using CensusApp.Api.Core.Domain.Model;
using CensusApp.Api.Core.Infra.Data.MongoDb.Extensions;
using MediatR;
using MongoDB.Driver;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Infra.Data.Queries
{
    public class ConsultarPercentualNomesPorRegiao : IRequest<object>
    {
        public CommandID<string> Regiao { get; set; }
        public class ConsultarPercentualNomesPorRegiaoHandler : IRequestHandler<ConsultarPercentualNomesPorRegiao, object>
        {
            private readonly IMongoDatabase _mongoDatabase;
            public ConsultarPercentualNomesPorRegiaoHandler(IMongoDatabase mongoDatabase)
            {
                _mongoDatabase = mongoDatabase;
            }

            public Task<object> Handle(ConsultarPercentualNomesPorRegiao request, CancellationToken cancellationToken)
            {
                var queryPorRegiao = _mongoDatabase.GetCollection<Pessoa>()
                    .AsQueryable(true)
                    .ToList();

                var group = queryPorRegiao
                    .GroupBy(x => x.Nome)
                    .Select(n => new
                    {
                        Nome = n.Key,
                        Percent = n.Count() * 100 / queryPorRegiao.Count()
                    });

                return Task.FromResult<object>(group);

            }
        }
    }
}
