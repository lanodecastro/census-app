using Flunt.Notifications;
using System;

namespace CensusApp.Api.Core.Domain
{
    public class Entity : Notifiable<Notification>
    {
        public string Id { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public DateTime? AlteradoEm { get; private set; }
        public DateTime? ExcluidoEm { get; private set; }

        public Entity(string id = null)
        {
            Id = id is null ? Guid.NewGuid().ToString() : id;
            CriadoEm = DateTime.Now;
        }
        public void Update()
        {
            AlteradoEm = DateTime.Now;
        }
        public void Delete()
        {
            ExcluidoEm = DateTime.Now;
        }
    }
}
