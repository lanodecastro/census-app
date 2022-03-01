using CensusApp.Api.Core.Domain.Commands._Base;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace CensusApp.Api.Core.Domain.Commands.CriarPessoa
{
    public class CriarPessoaRequest : NotifiableCommand<Notification, Guid>
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public Guid Escolaridade { get; set; }
        public Guid RacaCor { get; set; }
        public string Mae { get; set; }
        public string Pai { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                );
        }
    }

}
