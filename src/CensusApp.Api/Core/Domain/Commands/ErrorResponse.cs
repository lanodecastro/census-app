namespace CensusApp.Api.Core.Domain.Commands
{
    public class ErrorResponse : ICommandResponse
    {
        public object Data { get; }
        public ErrorResponse(object data)
        {
            Data = data;
        }
    }
}
