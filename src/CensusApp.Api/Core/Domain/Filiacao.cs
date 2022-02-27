using Flunt.Notifications;
using Flunt.Validations;

namespace CensusApp.Api.Core.Domain
{
    public class Filiacao : Notifiable<Notification>
    {
        public Filiacao(string nomeMae, string nomePai = null)
        {
            AddNotifications(new Contract<Notification>()
                  .Requires()
                  .IsNotNullOrEmpty(nomeMae, "nomeMae_is_not_null", "Nome da mãe não pode ser nulo")
                  );

            if (!IsValid) return;

            NomeMae = nomeMae;
            NomePai = nomePai;
        }

        public string NomeMae { get; private set; }
        public string NomePai { get; private set; }

    }
}
