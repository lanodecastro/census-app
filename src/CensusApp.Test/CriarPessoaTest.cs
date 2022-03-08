using AutoFixture;
using CensusApp.Api.Core.Domain;
using CensusApp.Api.Core.Domain.Commands;
using CensusApp.Api.Core.Domain.Commands.CriarPessoa;
using CensusApp.Api.Core.Domain.Model;
using CensusApp.Api.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System.Threading;
using Xunit;

namespace CensusApp.Test
{
    public class CriarPessoaTest
    {
        private CriarPessoaRequest criarPessoaRequest = new Fixture().Create<CriarPessoaRequest>();
        private Mock<IRepository<Escolaridade>> escolaridadeRepository = new Mock<IRepository<Escolaridade>>();
        private Mock<IRepository<RacaCor>> racaCorRepository = new Mock<IRepository<RacaCor>>();
        private Mock<IRepository<Regiao>> regiaoRepository = new Mock<IRepository<Regiao>>();
        private Mock<IRepository<Pessoa>> pessoaRepository = new Mock<IRepository<Pessoa>>();
        private Mock<RequestHandlerDelegate<ICommandResponse>> next = new Mock<RequestHandlerDelegate<ICommandResponse>>();
        private Mock<IHubContext<RealtimeMessages>> hubContext = new Mock<IHubContext<RealtimeMessages>>();

        public CriarPessoaTest()
        {
            escolaridadeRepository.Setup(e => e.Get(criarPessoaRequest.Escolaridade.Id)).Returns(new Escolaridade("Ensino médio"));
            racaCorRepository.Setup(e => e.Get(criarPessoaRequest.RacaCor.Id)).Returns(new RacaCor("Parda"));
            regiaoRepository.Setup(e => e.Get(criarPessoaRequest.Regiao.Id)).Returns(new Regiao("Norte"));
        }

        [Fact]
        public void Deve_adicionar_referencias_ao_contexto_quando_elas_forem_validas()
        {

            var requestContext = new RequestContext();


            var handler = new BuscarReferenciaHandler(escolaridadeRepository.Object, racaCorRepository.Object, regiaoRepository.Object, requestContext);

            handler.Handle(criarPessoaRequest, new CancellationToken(), next.Object);

            Assert.True(handler.IsValid);
            Assert.True(requestContext.ContainsKey("racaCor"));
            Assert.True(requestContext.ContainsKey("escolaridade"));
            Assert.True(requestContext.ContainsKey("regiao"));

        }

        [Fact]
        public void Deve_criar_e_retornar_pessoa_quando_a_request_e_o_contexto_forem_validos()
        {
            var requestContext = new RequestContext();
            requestContext.Add("escolaridade", new Escolaridade("Médio"));
            requestContext.Add("racaCor", new RacaCor("Branca"));
            requestContext.Add("regiao", new Regiao("Norte"));
            requestContext.Add("pai", "01");
            requestContext.Add("mae", "02");


            var criarPessoaHandler = new CriarPessoaHandler(pessoaRepository.Object, requestContext);
            criarPessoaHandler.Handle(criarPessoaRequest, new CancellationToken(), next.Object);

            Assert.True(requestContext.ContainsKey("pessoa"));
            Assert.NotNull(requestContext["pessoa"]);
        }
    }
}
