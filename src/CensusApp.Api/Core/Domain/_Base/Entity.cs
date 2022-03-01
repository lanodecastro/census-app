using Flunt.Notifications;
using System;

namespace CensusApp.Api.Core.Domain._Base
{
    public class Entity:Notifiable<Notification>
    {
        public Guid Id { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public DateTime AlteradoEm { get; private set; }
        public DateTime ExcluidoEm { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            CriadoEm = DateTime.Now;
        }
        protected void Update()
        {
            AlteradoEm = DateTime.Now;
        }
        protected void Delete()
        {
            ExcluidoEm = DateTime.Now;
        }
    }
}
