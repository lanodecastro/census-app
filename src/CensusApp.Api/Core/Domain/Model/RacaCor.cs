using CensusApp.Api.Core.Domain.Extensions;
using Flunt.Notifications;
using Flunt.Validations;

namespace CensusApp.Api.Core.Domain.Model
{
    public class RacaCor : Entity
    {
        public string Descricao { get; private set; }
        public RacaCor(string descricao, string id = null) : base(id)
        {

            AddNotifications(new Contract<Notification>()
                        .Requires()
                        .IsNotNullOrEmptyWithDefaultMessage("Descricao", descricao)
                        );

            if (!IsValid) return;

            Descricao = descricao;

        }

    }
}
