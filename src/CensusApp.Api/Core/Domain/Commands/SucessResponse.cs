namespace CensusApp.Api.Core.Domain.Commands
{
    public class SucessResponse:ICommandResponse
    {
        public object Data { get; }
        public SucessResponse(object data)
        {
            Data = data;
        }
    }
}
