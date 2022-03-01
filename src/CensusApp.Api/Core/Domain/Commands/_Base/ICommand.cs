using Flunt.Notifications;
using MediatR;
using System.Collections.Generic;

namespace CensusApp.Api.Core.Domain.Commands._Base
{
    public interface ICommand<TResponse> : IRequest<TResponse>
    {
        bool IsValid { get; }
        IReadOnlyCollection<Notification> Notifications { get; }
        TResponse Response { get; set; }
        void Validate();
    }
}
