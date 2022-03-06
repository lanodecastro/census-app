using AutoMapper;
using CensusApp.Api.Core.Domain.Commands.CriarPessoa;
using CensusApp.Api.Core.Domain.Model;
using CensusApp.Api.Core.Infra.Data.Queries;
using CensusApp.Api.Core.Infra.Data.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CensusApp.Api.Controllers
{
    [Route("api/pessoa")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMapper _mapper;
        public PessoaController(IMediator mediator, IMongoDatabase mongoDatabase, IMapper mapper)
        {
            _mediator = mediator;
            _mongoDatabase = mongoDatabase;
            _mapper = mapper;
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CriarPessoaRequest([FromBody] CriarPessoaRequest command)
        {
            await _mediator.Send(command);

            if (!command.IsValid)
                return BadRequest(command.Notifications);

            return Ok(command.Response);
        }

        [HttpGet]
        public IActionResult All()
        {
            var pessoas = _mongoDatabase.GetCollection<Pessoa>("Pessoa")
                .AsQueryable()
                .ToList();
            return Ok(_mapper.Map<IList<PessoaViewModel>>(pessoas));

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
