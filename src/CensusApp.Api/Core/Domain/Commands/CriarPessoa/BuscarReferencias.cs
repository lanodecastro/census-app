using CensusApp.Api.Core.Domain.Model;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Domain.Commands.CriarPessoa
{
    public class BuscarReferencias : Notifiable<Notification>, IPipelineBehavior<CriarPessoaRequest, ICommandResponse>
    {
        private readonly IRepository<Escolaridade> _escolaridadeRepository;
        private readonly IRepository<RacaCor> _racaCorRepository;
        private readonly IRepository<Regiao> _regiaoRepository;
        private readonly RequestContext _context;

        public BuscarReferencias(IRepository<Escolaridade> escolaridadeRepository, IRepository<RacaCor> racaCorRepository, IRepository<Regiao> regiaoRepository, RequestContext context)
        {
            _escolaridadeRepository = escolaridadeRepository;
            _racaCorRepository = racaCorRepository;
            _regiaoRepository = regiaoRepository;
            _context = context;
        }

        public Task<ICommandResponse> Handle(CriarPessoaRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ICommandResponse> next)
        {
            var escolaridade = _escolaridadeRepository.Get(request.Escolaridade.Id);
            var racaCor = _racaCorRepository.Get(request.RacaCor.Id);
            var regiao = _regiaoRepository.Get(request.Regiao.Id);

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(escolaridade, "escolaridade_invalid", "Referência inválida para o atributo Escolaridade ")
                .IsNotNull(racaCor, "racaCor_invalid", "Referência inválida para o atributo Raca/Cor")
                .IsNotNull(regiao, "regiao_invalid", "Referência inválida para o atributo Regiao")
                );

            if (!IsValid)
                return Task.FromResult<ICommandResponse>(new ErrorResponse(this.Notifications));

            _context.Add("escolaridade", escolaridade);
            _context.Add("racaCor", racaCor);
            _context.Add("regiao", regiao);

            return next();


        }
    }
}
