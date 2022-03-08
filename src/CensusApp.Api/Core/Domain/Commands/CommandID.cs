namespace CensusApp.Api.Core.Domain.Commands
{
    public class CommandID<T>
    {
        public T Id { get; set; }
        public CommandID()
        {

        }
        public CommandID(T value)
        {
            Id = value;
        }
    }
}
