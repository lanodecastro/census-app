using Flunt.Notifications;
using MediatR;
using System;

namespace CensusApp.Api.Core.Domain.Commands._Base
{
    public abstract class NotifiableCommand<TNotification, TResponse> : Notifiable<Notification>, ICommand<TResponse>,IRequest<TResponse>
    {
        public TResponse Response { get; set; }
        public abstract void Validate();
    }
}
