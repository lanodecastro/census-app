﻿using CensusApp.Api.Core.Domain._Base;
using CensusApp.Api.Core.Domain.Extensions;
using CensusApp.Api.Core.Domain.Vo;
using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;

namespace CensusApp.Api.Core.Domain
{
    public class Pessoa : Entity
    {
        public Description Nome { get; private set; }
        public Description Sobrenome { get; private set; }
        public RacaCor RacaCor { get; private set; }
        public Escolaridade Escolaridade { get; private set; }
        public Pessoa Mae { get; private set; }
        public Pessoa Pai { get; private set; }
        public IList<Pessoa> Filhos { get; private set; }
        public Pessoa(string nome, string sobrenome, RacaCor racaCor, Escolaridade escolaridade, Pessoa mae, Pessoa pai)
        {
            Nome = new Description(nome, "nome_is_not_null", "Nome", 3, 255);
            Sobrenome = new Description(sobrenome, "sobrenome_is_not_null", "Sobrenome", 3, 255);

            AddNotifications(new Contract<Notification>()
                  .Requires()
                  .IsNotNull(racaCor, "racaCor_is_not_null", "Raça/Cor não pode ser nula")
                  .IsNotNull(escolaridade, "escolaridade_is_not_null", "Escolaridade não pode ser nula")
                  .IsNotNull(mae, "mae_is_not_null", "Mãe não pode ser nula")
                  .Join(Nome, Sobrenome)
                  .JoinIsNotNull(mae)
                  .JoinIsNotNull(pai)
                  );

            if (!IsValid) return;

            RacaCor = racaCor;
            Escolaridade = escolaridade;
            Mae = mae;
            Pai = pai;

            Filhos = new List<Pessoa>();
        }
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
