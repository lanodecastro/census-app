using CensusApp.Api.Core.Domain.Commands._Base;
using CensusApp.Api.Core.Domain.Model;
using CensusApp.Api.Core.Infra.Data.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System;
using System.Linq;

namespace CensusApp.Api.Core.Domain.Commands.CriarPessoa
{
    public class CriarPessoaHandler : CommandHandler<CriarPessoaRequest, string>
    {
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Escolaridade> _escolaridadeRepository;
        private readonly IRepository<RacaCor> _racaCorRepository;
        private readonly IRepository<Regiao> _regiaoRepository;
        private readonly IMediator _mediator;
        public CriarPessoaHandler(IRepository<Pessoa> pessoaRepository, IRepository<Escolaridade> escolaridadeRepository, IRepository<RacaCor> racaCorRepository, IRepository<Regiao> regiaoRepository, IMediator mediator)
        {
            _pessoaRepository = pessoaRepository;
            _escolaridadeRepository = escolaridadeRepository;
            _racaCorRepository = racaCorRepository;
            _regiaoRepository = regiaoRepository;
            _mediator = mediator;
        }
        protected override void PrepareCommand(CriarPessoaRequest command)
        {
            var referencias = ResolverReferencias(command.Escolaridade.Id, command.RacaCor.Id, command.Regiao.Id);

            if (!command.IsValid) return;

            var escolaridade = referencias.Item1;
            var racaCor = referencias.Item2;
            var regiao = referencias.Item3;


            var pai = _mediator.Send(new ConsultarPessoa()
            {
                NomeSobrenome = command.NomePai,
                IgnoreDeletedItens = false
            }).Result.FirstOrDefault();

            var mae = _mediator.Send(new ConsultarPessoa()
            {
                NomeSobrenome = command.NomeMae,
                IgnoreDeletedItens=false
            }).Result.FirstOrDefault();


            command.AddNotifications(new Contract<Notification>()
                        .Requires()
                        .IsNotNull(pai, "pai_is_not_null", "Pai não encontrado")
                        .IsNotNull(mae, "mae_is_not_null", "Mãe não encontrada"));

            if (!command.IsValid) return;

            var pessoa = new Pessoa(command.Nome, command.Sobrenome, racaCor, escolaridade, regiao, pai.Id, mae.Id);
            command.AddNotifications(pessoa);

            if (!command.IsValid) return;

            _pessoaRepository.Insert(pessoa);

            command.Response = pessoa.Id;
            
        }
        private Tuple<Escolaridade, RacaCor, Regiao> ResolverReferencias(string idEscolaridade, string idRacaCor, string idRegiao)
        {
            var escolaridade = _escolaridadeRepository.Get(idEscolaridade);
            var racaCor = _racaCorRepository.Get(idRacaCor);
            var regiao = _regiaoRepository.Get(idRegiao);

            //command.AddNotifications(new Contract<Notification>()
            //             .Requires()
            //             .IsNotNull(escolaridade, "escolaridade_invalid", "Referência inválida para o atributo Escolaridade ")
            //             .IsNotNull(racaCor, "racaCor_invalid", "Referência inválida para o atributo Raca/Cor")
            //             .IsNotNull(regiao, "regiao_invalid", "Referência inválida para o atributo Regiao")
            //             );

            return new Tuple<Escolaridade, RacaCor, Regiao>(escolaridade, racaCor, regiao);
        }

    }
}
