using AutoMapper;
using CensusApp.Api.Core.Domain.Model;
using CensusApp.Api.Core.Infra.Data.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using PainelOuvidoria.Api.Hubs;
using System.Threading;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Domain.Commands.CriarPessoa
{
    public class Notificar : IPipelineBehavior<CriarPessoaRequest, ICommandResponse>
    {
        private readonly IHubContext<RealtimeMessages> _hubContext;
        private readonly RequestContext _context;
        private readonly IMapper _mapper;
        public Notificar(IHubContext<RealtimeMessages> hubContext, RequestContext context, IMapper mapper)
        {
            _hubContext = hubContext;
            _context = context;
            _mapper = mapper;
        }

        public Task<ICommandResponse> Handle(CriarPessoaRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ICommandResponse> next)
        {
            var pessoa = (Pessoa)_context["pessoa"];
            _hubContext.Clients.All.SendAsync("notify", _mapper.Map<PessoaViewModel>(pessoa));

            return Task.FromResult<ICommandResponse>(new SucessResponse(pessoa.Id));
        }
    }
}
