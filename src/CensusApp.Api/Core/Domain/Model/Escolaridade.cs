using CensusApp.Api.Core.Domain.Extensions;
using Flunt.Notifications;
using Flunt.Validations;

namespace CensusApp.Api.Core.Domain.Model
{
    public class Escolaridade : Entity
    {
        public string Descricao { get; private set; }
        public Escolaridade(string descricao, string id = null) : base(id)
        {

            AddNotifications(new Contract<Notification>()
                        .Requires()
                        .IsNotNullOrEmptyWithDefaultMessage("Descricao",descricao)
                        );

            if (!IsValid) return;

            Descricao = descricao;

        }

    }

}