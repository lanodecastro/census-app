using CensusApp.Api.Core.Domain.Commands._Base;
using Flunt.Notifications;
using Flunt.Validations;

namespace CensusApp.Api.Core.Domain.Commands.CriarPessoa
{
    public class CriarPessoaRequest : NotifiableCommand<Notification, string>
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public CommandID<string> Escolaridade { get; set; }
        public CommandID<string> RacaCor { get; set; }
        public CommandID<string> Regiao { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public override void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Nome, "", "Nome")
                .IsNotNullOrEmpty(Sobrenome, "", "Sobrenome")
                .IsNotNull(Escolaridade, "", "Escolaridade")
                .IsNotNull(RacaCor, "", "RacaCor")
                .IsNotNull(Regiao, "", "Regiao")
                ); 
        }
    }

}
