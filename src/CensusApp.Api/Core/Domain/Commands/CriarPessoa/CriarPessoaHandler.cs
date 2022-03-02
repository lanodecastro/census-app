using CensusApp.Api.Core.Domain._Base;
using CensusApp.Api.Core.Domain.Commands._Base;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace CensusApp.Api.Core.Domain.Commands.CriarPessoa
{
    public class CriarPessoaHandler : CommandHandler<CriarPessoaRequest, string>
    {
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Escolaridade> _escolaridadeRepository;
        private readonly IRepository<RacaCor> _racaCorRepository;
        public CriarPessoaHandler(IRepository<Pessoa> pessoaRepository, IRepository<Escolaridade> escolaridadeRepository, IRepository<RacaCor> racaCorRepository)
        {
            _pessoaRepository = pessoaRepository;
            _escolaridadeRepository = escolaridadeRepository;
            _racaCorRepository = racaCorRepository;
        }

        protected override void PrepareCommand(CriarPessoaRequest command)
        {
            var mae = _pessoaRepository.Get(command.Mae);
            var escolaridade = _escolaridadeRepository.Get(command.Escolaridade);
            var racaCor = _racaCorRepository.Get(command.RacaCor);
            Pessoa pai = null;

            command.AddNotifications(new Contract<Notification>()
                         .Requires()
                         .IsNotNull(mae, "mae_valid", "Referência inválida para o atributo Mae")
                         .IsNotNull(escolaridade, "escolaridade_valid", "Referência inválida para o atributo Escolaridade ")
                         .IsNotNull(racaCor, "racaCor_valid", "Referência inválida para o atributo Raca/Cor inválida")
                         );

            if (string.IsNullOrEmpty(command.Pai))
            {
                pai = _pessoaRepository.Get(command.Pai);
                if (pai is null)
                    command.AddNotification("pai_valid", "Referência inválida para o atributo Pai");
            }

            if (!command.IsValid) return;

            var pessoa = new Pessoa(command.Nome, command.Sobrenome, racaCor, escolaridade, mae, pai);

            _pessoaRepository.Add(pessoa);

            command.Response = pessoa.Id;
        }
        private Tuple<string, string> ExtrairNomeSobrenome(string nomeSobrenome)
        {
            var nomeSobrenomeArray = nomeSobrenome.Split(' ');

            if (nomeSobrenomeArray.Length != 2)
                return null;

            return new Tuple<string, string>(nomeSobrenomeArray[0], nomeSobrenomeArray[1]);
        }
    }
}
