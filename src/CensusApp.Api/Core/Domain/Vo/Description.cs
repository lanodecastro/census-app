using CensusApp.Api.Core.Domain._Base;
using Flunt.Notifications;
using Flunt.Validations;

namespace CensusApp.Api.Core.Domain.Vo
{
    public class Description : ValueObject
    {
        public string Value { get; private set; }
        public int Maxlength { get; private set; }
        public int Minlength { get; private set; }
        public Description(string val, string key, string prop, int? minlength=1, int? maxlength = 255) :base(prop)
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(prop,key, $"{Property} não pode ser nulo")
                .IsNotNullOrEmpty(val, key, $"{Property} não pode ser nulo")
                .IsGreaterThan(LengthWhenIsNotNull(val), minlength.Value -1, "minlength_valid", $"{Property} deve conter no mínimo {minlength.Value} caracter(es).")
                .IsLowerThan(LengthWhenIsNotNull(val), maxlength.Value -1, "maxlength_valid", $"{Property} deve conter no máximo {maxlength.Value} caracter(es).")
                );

            if (!IsValid) return;

            this.Value = val;
            this.Maxlength = maxlength.Value;
            this.Minlength = minlength.Value;
        }
        protected Description() { }
    }
}
