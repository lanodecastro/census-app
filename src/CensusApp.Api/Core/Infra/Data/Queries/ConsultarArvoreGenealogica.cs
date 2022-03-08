using AutoMapper;
using CensusApp.Api.Core.Domain.Model;
using CensusApp.Api.Core.Infra.Data.MongoDb.Extensions;
using CensusApp.Api.Core.Infra.Data.Queries.ViewModels;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Infra.Data.Queries
{
    public class ConsultarArvoreGenealogica : IRequest<object>
    {
        public string Id { get; set; }
        public int Nivel { get; set; }

        public class ConsultarArvoreGenealogicaHandler : IRequestHandler<ConsultarArvoreGenealogica, object>
        {
            private readonly IMongoCollection<Pessoa> _mongoCollection;
            private readonly IMapper _mapper;
            private int nivel = 0;

            public ConsultarArvoreGenealogicaHandler(IMongoDatabase monogoDatabase, IMapper mapper)
            {
                _mongoCollection = monogoDatabase.GetCollection<Pessoa>();
                _mapper = mapper;
            }

            public Task<object> Handle(ConsultarArvoreGenealogica request, CancellationToken cancellationToken)
            {
                var pessoas = _mongoCollection
                    .AsQueryable(true)
                    .ToList();

                List<PessoaViewModelArvore> arvore = new List<PessoaViewModelArvore>();
                var root = new PessoaViewModelArvore() { Id = request.Id };
                nivel = request.Nivel;
                GetChildren(root, _mapper.Map<List<PessoaViewModelArvore>>(pessoas));


                return Task.FromResult<object>(root);
            }
            private void GetChildren(PessoaViewModelArvore root, List<PessoaViewModelArvore> data)
            {
                var i = 0;
                var children = data.Where(x => x.IdPai == root.Id || x.IdMae == root.Id);

                foreach (var item in children)
                {
                    root.Filhos.Add(item);
                    i++;

                    if (i < nivel)
                        GetChildren(item, data);

                }

            }

        }
    }
}
