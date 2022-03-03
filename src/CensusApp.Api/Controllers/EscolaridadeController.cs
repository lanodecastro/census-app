using CensusApp.Api.Core.Domain;
using CensusApp.Api.Core.Domain._Base;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CensusApp.Api.Controllers
{
    [ApiController,Route("api/escolaridade")]
    public class EscolaridadeController:ControllerBase
    {
        private readonly IRepository<Escolaridade> repository;
        public EscolaridadeController(IRepository<Escolaridade> repository)
        {
            this.repository = repository;
        }


        [HttpGet, Route("")]
        public IActionResult AddEscolaridade(string desc)
        {
            var escolaridade = new Escolaridade(desc);
            repository.Add(escolaridade);
            return Ok(escolaridade);
        }

    }
}
