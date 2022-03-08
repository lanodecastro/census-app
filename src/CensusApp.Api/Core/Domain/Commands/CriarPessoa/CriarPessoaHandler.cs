using CensusApp.Api.Core.Domain.Commands;
using CensusApp.Api.Core.Domain.Commands.CriarPessoa;
using CensusApp.Api.Core.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Domain.requests.CriarPessoa
{
    public class CriarPessoaHandler : IPipelineBehavior<CriarPessoaRequest, ICommandResponse>
    {
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly RequestContext _context;

        public CriarPessoaHandler(IRepository<Pessoa> pessoaRepository, RequestContext context)
        {
            _pessoaRepository = pessoaRepository;
            _context = context;
        }

        public Task<ICommandResponse> Handle(CriarPessoaRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ICommandResponse> next)
        {
            var racaCor = (RacaCor)_context["racaCor"];
            var escolaridade = (Escolaridade)_context["escolaridade"];
            var regiao = (Regiao)_context["regiao"];
            var pai = _context["pai"].ToString();
            var mae = _context["mae"].ToString();

            var pessoa = new Pessoa(request.Nome, request.Sobrenome, racaCor, escolaridade, regiao, pai, mae);

            if (!pessoa.IsValid)
                return Task.FromResult<ICommandResponse>(new ErrorResponse(pessoa.Notifications));

            _pessoaRepository.Insert(pessoa);
            _context.Add("pessoa", pessoa);
            return next();

        }

    }
}
