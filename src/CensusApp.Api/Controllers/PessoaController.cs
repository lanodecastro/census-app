using CensusApp.Api.Core.Domain.Commands.CriarPessoa;
using CensusApp.Api.Core.Infra.Data.Queries;
using CensusApp.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PainelOuvidoria.Api.Hubs;
using System.Threading.Tasks;

namespace CensusApp.Api.Controllers
{
    [Route("api/pessoa")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PessoaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CriarPessoaRequest([FromBody] CriarPessoaRequest command)
        {
            var response = await _mediator.Send(command);
            return response.CreateHttpResponse();
        }

        [HttpPost, Route("query")]
        public async Task<IActionResult> ConsultarPessoa([FromBody] ConsultarPessoa query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost, Route("query2")]
        public async Task<IActionResult> ConsultarPercentualNomesPorRegiao([FromBody] ConsultarPercentualNomesPorRegiao query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet, Route("arvore")]
        public async Task<IActionResult> ConsultarArvoreGenealogica()
        {
            var command = new ConsultarArvoreGenealogica();

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
