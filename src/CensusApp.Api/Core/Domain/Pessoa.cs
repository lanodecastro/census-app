using CensusApp.Api.Core.Domain._Base;
using CensusApp.Api.Core.Domain.Extensions;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace CensusApp.Api.Core.Domain
{
    public class Pessoa : Entity<Notification>
    {
        public Pessoa(string nome, string sobrenome, RacaCorEnum? racaCor, EscolaridadeEnum? escolaridade, Filiacao filiacao)
        {

            AddNotifications(new Contract<Notification>()
                  .Requires()
                  .IsNotNullOrEmpty(nome, "nome_is_not_null", "Nome não pode ser nulo")
                  .IsNotNullOrEmpty(sobrenome, "sobrenome_is_not_null", "Sobrenome não pode ser nulo")
                  .IsNotNull(racaCor, "racaCor_is_not_null", "Raça/Cor não pode ser nula")
                  .IsNotNull(escolaridade, "escolaridade_is_not_null", "Escolaridade não pode ser nula")
                  .IsNotNull(filiacao, "filiacao_is_not_null", "Filiação não pode ser nula")
                  .JoinIsNotNull(filiacao)
                  );

            if (!IsValid) return;

            Id = Guid.NewGuid();
            Nome = nome;
            Sobrenome = sobrenome;
            RacaCor = racaCor;
            Escolaridade = escolaridade;
            Filiacao = filiacao;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public RacaCorEnum? RacaCor { get; private set; }
        public EscolaridadeEnum? Escolaridade { get; private set; }
        public Filiacao Filiacao { get; private set; }
        public IList<Pessoa> Filhos { get; private set; }

        public void AddFilho(Pessoa filho)
        {
            AddNotifications(new Contract<Notification>()
                  .Requires()
                  .IsNotNull(filho, "filho_is_not_null", "Filho não pode ser nulo")
                  .JoinIsNotNull(filho)
                  );

            if (!IsValid) return;
            
            Filhos.Add(filho);

        }

    }
}
