using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;

namespace CensusApp.Api.Config.MongoDb
{
    public static class MongoDbConfigurationExtension
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConfigSection = configuration.GetSection("MongoDb");
            var mongoDbConfig = mongoConfigSection.Get<MongoDbConfig>();

            services.AddSingleton<IMongoClient>(new MongoClient(new MongoClientSettings()
            {
                ConnectTimeout = TimeSpan.FromSeconds(10),
                Server = new MongoServerAddress(mongoDbConfig.Server, mongoDbConfig.Port),
                Credential = MongoCredential.CreateCredential(mongoDbConfig.Database, mongoDbConfig.Username, mongoDbConfig.Password)
            }));

            services.AddSingleton<IMongoDatabase>(scope => scope.GetService<IMongoClient>().GetDatabase(mongoDbConfig.Database));
        }
    }
}
