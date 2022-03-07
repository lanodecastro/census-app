using CensusApp.Api.Core.Infra.Data.Queries;
using CensusApp.Api.Core.Infra.Data.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensusApp.Api.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var query = new ConsultarPessoa();
            var response = await _mediator.Send(query);

            var data = new
            {
                regiao = await Select(response.GroupBy(x => x.Regiao)),
                escolaridade = await Select(response.GroupBy(x => x.Escolaridade)),
                racaCor = await Select(response.GroupBy(x => x.RacaCor)),
                total = response.Count()
            };

            return Ok(data);

        }

        private async Task<object> Select(IEnumerable<IGrouping<string, PessoaViewModel>> group)
        {
            return group.Select(x => new
            {
                label=x.Key,
                value=x.Count()

            });           
        }
    }

}
