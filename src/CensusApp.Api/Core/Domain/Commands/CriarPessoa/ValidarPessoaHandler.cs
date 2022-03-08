using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Domain.Commands.CriarPessoa
{
    public class ValidarPessoaHandler : Notifiable<Notification>, IPipelineBehavior<CriarPessoaRequest, ICommandResponse>
    {
        public Task<ICommandResponse> Handle(CriarPessoaRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ICommandResponse> next)
        {
            AddNotifications(new Contract<Notification>()
               .Requires()
               .IsNotNullOrEmpty(request.Nome, "", "Nome")
               .IsNotNullOrEmpty(request.Sobrenome, "", "Sobrenome")
               .IsNotNull(request.Escolaridade, "", "Escolaridade")
               .IsNotNull(request.RacaCor, "", "RacaCor")
               .IsNotNull(request.Regiao, "", "Regiao")
               );

            if (IsValid)
                return next();

            return Task.FromResult<ICommandResponse>(new ErrorResponse(this.Notifications));

        }
    }
}
