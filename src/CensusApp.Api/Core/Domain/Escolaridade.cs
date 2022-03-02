using CensusApp.Api.Core.Domain._Base;
using CensusApp.Api.Core.Domain.Vo;

namespace CensusApp.Api.Core.Domain
{
    public class Escolaridade : Entity
    {
        public Description Descricao { get; private set; }
        public Escolaridade(string descricao, string id = null) : base(id)
        {
            Descricao = new Description(descricao, "descricao_is_not_null", "Descricação", 1, 100);
        }

    }

}