using MediatR;
using System.Threading.Tasks;

namespace CensusApp.Api.Core.Domain.Commands._Base
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}
