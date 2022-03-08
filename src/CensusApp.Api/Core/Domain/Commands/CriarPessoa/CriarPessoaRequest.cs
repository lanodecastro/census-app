using MediatR;

namespace CensusApp.Api.Core.Domain.Commands.CriarPessoa
{
    public class CriarPessoaRequest : IRequest<ICommandResponse>
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public CommandID<string> Escolaridade { get; set; }
        public CommandID<string> RacaCor { get; set; }
        public CommandID<string> Regiao { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }

    }

}
