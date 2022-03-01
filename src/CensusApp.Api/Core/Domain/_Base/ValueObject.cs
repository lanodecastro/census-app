using Flunt.Notifications;

namespace CensusApp.Api.Core.Domain._Base
{
    public abstract class ValueObject : Notifiable<Notification>
    {
        protected string Property { get; private set; }
        public ValueObject(string property = null)
        {

            if (!string.IsNullOrEmpty(property))
                Property = property;
        }
        protected int LengthWhenIsNotNull(string val)
        {
            if (val is null)
                return 0;

            return val.Length;
        }
    }
}
