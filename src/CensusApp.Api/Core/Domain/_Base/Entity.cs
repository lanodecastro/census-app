using Flunt.Notifications;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CensusApp.Api.Core.Domain._Base
{
    public class Entity:Notifiable<Notification>
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public DateTime AlteradoEm { get; private set; }
        public DateTime ExcluidoEm { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
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
