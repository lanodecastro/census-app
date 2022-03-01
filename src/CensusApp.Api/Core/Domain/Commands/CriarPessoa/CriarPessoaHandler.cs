using CensusApp.Api.Core.Domain._Base;
using CensusApp.Api.Core.Domain.Commands._Base;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace CensusApp.Api.Core.Domain.Commands.CriarPessoa
{
    public class CriarPessoaHandler : CommandHandler<CriarPessoaRequest, Guid>
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
            var escolaridade = _escolaridadeRepository.Get(command.Escolaridade);
            var racaCor = _racaCorRepository.Get(command.RacaCor);

            command.AddNotifications(new Contract<Notification>()
                         .Requires()
                         .IsNotNull(escolaridade, "escolaridade_valid", "Escolaridade inválida")
                         .IsNotNull(racaCor, "racaCor_valid", "Raca/Cor inválida")
                         );

            if (!command.IsValid) return;

            var pessoa = new Pessoa(command.Nome, command.Sobrenome, racaCor, escolaridade, new Filiacao(command.Mae, command.Pai));

            _pessoaRepository.Add(pessoa);
            
            command.Response = pessoa.Id;
        }
    }
}
