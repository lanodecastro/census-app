using System.Collections.Generic;

namespace CensusApp.Api.Core.Infra.Data.Queries.ViewModels
{
    public class PessoaViewModelArvore
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string IdPai { get; set; }
        public string IdMae { get; set; }
        public List<PessoaViewModelArvore> Filhos { get; set; }

        public PessoaViewModelArvore()
        {
            Filhos = new List<PessoaViewModelArvore>();
        }

    }
}
