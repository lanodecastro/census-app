using CensusApp.Api.Core.Domain.Extensions;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Domain.Model
{
    public class Regiao:Entity
    {
        public string Descricao { get; private set; }
        public Regiao(string descricao, string id = null) : base(id)
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
