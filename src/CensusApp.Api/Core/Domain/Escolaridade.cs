using CensusApp.Api.Core.Domain._Base;
using CensusApp.Api.Core.Domain.Vo;

namespace CensusApp.Api.Core.Domain
{
    [BsonCollection("Escolaridade")]
    public class Escolaridade : Entity
    {
        public Escolaridade(string descricao)
        {
            Descricao = new Description(descricao, "descricao_is_not_null","Descricação",1, 100);
        }
        public Description Descricao { get; private set; }
    }
    
   
}
