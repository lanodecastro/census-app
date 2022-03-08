using CensusApp.Api.Core.Domain.Model;
using CensusApp.Api.Core.Infra.Data.MongoDb.Extensions;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Infra.Data.Queries
{
    public class ConsultarArvoreGenealogica : IRequest<object>
    {
        public string Nome { get; set; }

        public class ConsultarArvoreGenealogicaHandler : IRequestHandler<ConsultarArvoreGenealogica, object>
        {
            private readonly IMongoCollection<Pessoa> _mongoCollection;

            public ConsultarArvoreGenealogicaHandler(IMongoDatabase monogoDatabase)
            {
                _mongoCollection = monogoDatabase.GetCollection<Pessoa>();
            }

            public Task<object> Handle(ConsultarArvoreGenealogica request, CancellationToken cancellationToken)
            {
                //var collection = 
                //    .AsQueryable()
                //    .ToList();


                return Task.FromResult<object>(new { });
            }

        }
    }
}
