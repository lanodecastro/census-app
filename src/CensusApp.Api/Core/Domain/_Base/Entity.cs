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
        public string Id { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public DateTime AlteradoEm { get; private set; }
        public DateTime ExcluidoEm { get; private set; }

        public Entity(string id=null)
        {
            if(id is null)
                Id = Guid.NewGuid().ToString();

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
