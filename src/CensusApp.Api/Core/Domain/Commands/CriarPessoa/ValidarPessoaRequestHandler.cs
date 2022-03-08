using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Domain.Commands.CriarPessoa
{
    public class ValidarPessoaRequestHandler : Notifiable<Notification>, IPipelineBehavior<CriarPessoaRequest, ICommandResponse>
    {
        public Task<ICommandResponse> Handle(CriarPessoaRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ICommandResponse> next)
        {
            AddNotifications(new Contract<Notification>()
               .Requires()
               .IsNotNullOrEmpty(request.Nome, "nome_is_not_null", "Nome é um atributo obrigatório")
               .IsNotNullOrEmpty(request.Sobrenome, "sobrenome_is_not_null", "Sobrenome é um atributo obrigatório")
               .IsNotNull(request.Escolaridade, "escolaridade_is_not_null", "Escolaridade é um atributo obrigatório")
               .IsNotNull(request.RacaCor, "racaCor_is_not_null", "Raca/Cor é um atributo obrigatório")
               .IsNotNull(request.Regiao, "regiao_is_not_null", "Região é um atributo obrigatório")
               .IsNotNull(request.NomePai, "nomePai_is_not_null", "Nome do Pai é um atributo obrigatório")
               .IsNotNull(request.NomeMae, "nomeMae_is_not_null", "Nome da Mãe é um atributo obrigatório")
               );

            if (IsValid)
                return next();

            return Task.FromResult<ICommandResponse>(new ErrorResponse(this.Notifications));

        }
    }
}
