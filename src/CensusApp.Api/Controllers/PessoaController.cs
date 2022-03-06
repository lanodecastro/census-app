using AutoMapper;
using CensusApp.Api.Core.Domain.Commands.CriarPessoa;
using CensusApp.Api.Core.Domain.Model;
using CensusApp.Api.Core.Infra.Data.Queries;
using CensusApp.Api.Core.Infra.Data.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;
using PainelOuvidoria.Api.Hubs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CensusApp.Api.Controllers
{
    [Route("api/pessoa")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IMediator _mediator;       
        private readonly IHubContext<RealtimeMessages> _hubContext;

        public PessoaController(IMediator mediator, IHubContext<RealtimeMessages> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CriarPessoaRequest([FromBody] CriarPessoaRequest command)
        {
            await _mediator.Send(command);

            if (!command.IsValid)
                return BadRequest(command.Notifications);

            await _hubContext.Clients.All.SendAsync("notify",command.Response);

            return Ok(command.Response);
        }

        [HttpPost,Route("query")]
        public async Task<IActionResult> ConsultarPessoa([FromBody] ConsultarPessoa query)
        {
            var result=await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost, Route("query2")]
        public async Task<IActionResult> ConsultarPercentualNomesPorRegiao([FromBody] ConsultarPercentualNomesPorRegiao query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

    }
}
