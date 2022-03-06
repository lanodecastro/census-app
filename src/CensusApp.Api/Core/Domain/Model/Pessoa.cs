using CensusApp.Api.Core.Domain.Extensions;
using Flunt.Notifications;
using Flunt.Validations;

namespace CensusApp.Api.Core.Domain.Model
{
    public class Pessoa : Entity
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public RacaCor RacaCor { get; private set; }
        public Escolaridade Escolaridade { get; private set; }
        public Regiao Regiao { get; private set; }
        public string IdPai { get; private set; }
        public string IdMae { get; private set; }

        private Pessoa() : base("0000")
        {
            Nome = "NA";
            Sobrenome = "NA";
            Delete();
        }
        public static Pessoa CreateRoot()
        {
            return new Pessoa();
        }
        public Pessoa(string nome, string sobrenome, RacaCor racaCor, Escolaridade escolaridade, Regiao regiao, string idPai, string idMae)
        {
            AddNotifications(new Contract<Notification>()
                  .Requires()
                  .IsNotNullOrEmptyWithDefaultMessage("Nome", nome)
                  .IsNotNullOrEmptyWithDefaultMessage("Sobrenome", sobrenome)
                  .IsNotNullWithDefaultMessage("RacaCor", racaCor)
                  .IsNotNullWithDefaultMessage("Escolaridade", escolaridade)
                  .IsNotNullWithDefaultMessage("Regiao", regiao)
                  );

            if (!IsValid) return;

            Nome = nome;
            Sobrenome = sobrenome;
            RacaCor = racaCor;
            Escolaridade = escolaridade;
            Regiao = regiao;
            IdPai = idPai;
            IdMae = idMae;
        }

    }
}
