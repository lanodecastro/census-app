using CensusApp.Api.Core.Domain.Commands.CriarPessoa;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult CriarPessoaRequest([FromBody] CriarPessoaRequest command)
        {
            _mediator.Send(command);
            return Ok(command);
        }
        
    }
}
