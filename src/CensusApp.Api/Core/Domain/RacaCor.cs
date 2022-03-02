using CensusApp.Api.Core.Domain._Base;
using CensusApp.Api.Core.Domain.Vo;

namespace CensusApp.Api.Core.Domain
{
    public class RacaCor : Entity
    {
        public Description Descricao { get; private set; }
        public RacaCor(string descricao, string id = null) : base(id)
        {
            Descricao = new Description(descricao, "descricao_is_not_null", "Descrição", 3, 50);
        }

    }
}
