using CensusApp.Api.Core.Infra.Data.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Domain.Commands.CriarPessoa
{
    public class VincularFiliacao : Notifiable<Notification>, IPipelineBehavior<CriarPessoaRequest, ICommandResponse>
    {
        private readonly IMediator _mediator;
        private readonly RequestContext _context;
        public VincularFiliacao(IMediator mediator, RequestContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public Task<ICommandResponse> Handle(CriarPessoaRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ICommandResponse> next)
        {
            var pai = _mediator.Send(new ConsultarPessoa()
            {
                NomeSobrenome = request.NomePai,
                IgnoreDeletedItens = false
            }).Result.Rows.FirstOrDefault();

            var mae = _mediator.Send(new ConsultarPessoa()
            {
                NomeSobrenome = request.NomeMae,
                IgnoreDeletedItens = false
            }).Result.Rows.FirstOrDefault();

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(pai, "pai_is_not_null", "Pai não encontrado")
                .IsNotNull(mae, "mae_is_not_null", "Mãe não encontrada"));

            if (!IsValid)
                return Task.FromResult<ICommandResponse>(new ErrorResponse(this.Notifications));

            _context.Add("pai", pai.Id);
            _context.Add("mae", mae.Id);
            return next();
        }
    }
}
