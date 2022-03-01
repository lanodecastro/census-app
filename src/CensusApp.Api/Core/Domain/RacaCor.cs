using CensusApp.Api.Core.Domain._Base;
using CensusApp.Api.Core.Domain.Vo;

namespace CensusApp.Api.Core.Domain
{
    [BsonCollection("RacaCor")]
    public class RacaCor : Entity
    {
        public RacaCor(string descricao)
        {
            Descricao = new Description(descricao, "descricao_is_not_null", "Descrição", 3, 50);
        }
        public Description Descricao { get; private set; }

    }
}
