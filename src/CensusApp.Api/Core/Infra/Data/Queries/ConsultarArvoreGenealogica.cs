using CensusApp.Api.Core.Domain.Model;
using CensusApp.Api.Core.Infra.Data.MongoDb.Extensions;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Infra.Data.Queries
{
    public class ConsultarArvoreGenealogica : IRequest<object>
    {
        public string Nome { get; set; }


        public class ConsultarArvoreGenealogicaHandler : IRequestHandler<ConsultarArvoreGenealogica, object>
        {
            private readonly IMongoDatabase _monogoDatabase;

            public ConsultarArvoreGenealogicaHandler(IMongoDatabase monogoDatabase)
            {
                _monogoDatabase = monogoDatabase;
            }

            public Task<object> Handle(ConsultarArvoreGenealogica request, CancellationToken cancellationToken)
            {
                var collection = _monogoDatabase.GetCollection<Pessoa>();

                //collection.Aggregate([new AggregateGraphLookupOptions<Pessoa>()
                //{

                //}]);

                return Task.FromResult(new object());
            }
        }
    }
}
