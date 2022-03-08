using AutoMapper;
using CensusApp.Api.Core.Domain.Commands;
using CensusApp.Api.Core.Domain.Model;
using CensusApp.Api.Core.Infra.Data.MongoDb.Extensions;
using CensusApp.Api.Core.Infra.Data.Queries.Extensions;
using CensusApp.Api.Core.Infra.Data.Queries.ViewModels;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Infra.Data.Queries
{
    public class ConsultarPessoa : IRequest<QueryResponse<IList<PessoaViewModel>>>
    {
        public CommandID<string> Escolaridade { get; set; }
        public CommandID<string> RacaCor { get; set; }
        public CommandID<string> Regiao { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string NomeSobrenome { get; set; }
        public bool? IgnoreDeletedItens { get; set; }

        public class ConsultarPessoaHandler : IRequestHandler<ConsultarPessoa, QueryResponse<IList<PessoaViewModel>>>
        {
            private readonly IMongoDatabase _mongoDatabase;
            private readonly IMapper _mapper;

            public ConsultarPessoaHandler(IMongoDatabase mongoDatabase, IMapper mapper)
            {
                _mongoDatabase = mongoDatabase;
                _mapper = mapper;
            }

            public Task<QueryResponse<IList<PessoaViewModel>>> Handle(ConsultarPessoa request, CancellationToken cancellationToken)
            {

                var query = _mongoDatabase.GetCollection<Pessoa>()
                   .AsQueryable(request.IgnoreDeletedItens.HasValue ? request.IgnoreDeletedItens.Value : true)
                       .Nome(request.Nome)
                       .Sobrenome(request.Sobrenome)
                       .NomeSobrenome(request.NomeSobrenome)
                       .Escolaridade(request.Escolaridade?.Id)
                       .RacaCor(request.RacaCor?.Id)
                       .Regiao(request.Regiao?.Id)
                   .ToList();

                var result = _mapper.Map<IList<PessoaViewModel>>(query);

                return Task.FromResult(new QueryResponse<IList<PessoaViewModel>>(result, result.Count));
            }
        }

    }
}
