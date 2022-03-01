using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Domain.Commands._Base
{
    public abstract class CommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : IRequest<TResponse>, ICommand<TResponse>
    {
        public Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();

                if (!request.IsValid)
                    return Task.FromResult<TResponse>(request.Response);

                PrepareCommand(request);
                return Task.FromResult<TResponse>(request.Response);
            }
            catch (Exception ex)
            {
                return Task.FromException<TResponse>(ex);
            }

        }
        protected abstract void PrepareCommand(TCommand command);


    }
}
