using System.Collections.Generic;

namespace CensusApp.Api.Core.Infra.Data.Queries.ViewModels
{
    public class PessoaViewModelArvore
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public KeyValuePair<string,string> Pai { get; set; }
        public KeyValuePair<string, string> Mae { get; set; }
        
    }
}
