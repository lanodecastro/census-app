using CensusApp.Api.Core.Domain.Model;
using CensusApp.Api.Core.Infra.Data.MongoDb;
using CensusApp.Api.Core.Infra.Data.MongoDb.Extensions;
using CensusApp.Api.Core.Infra.Data.MongoDb.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace CensusApp.Api.Config.MongoDb
{
    public static class MongoDbConfigurationExtension
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConfigSection = configuration.GetSection("MongoDb");
            var mongoDbConfig = mongoConfigSection.Get<MongoDbConfig>();
            var mongoClientSettings = new MongoClientSettings()
            {
                ConnectTimeout = TimeSpan.FromSeconds(10),
                Server = new MongoServerAddress(mongoDbConfig.Server, mongoDbConfig.Port),
                Credential = MongoCredential.CreateCredential(mongoDbConfig.Database, mongoDbConfig.Username, mongoDbConfig.Password)
            };

            services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));

            services.AddSingleton<IMongoDatabase>(scope => scope.GetService<IMongoClient>()
                .GetDatabase(mongoDbConfig.Database)
                .InitData());

            services.AddHealthChecks()
                .AddMongoDb(mongoClientSettings,
                   name: "mongo",
                   failureStatus: HealthStatus.Unhealthy);

            services.AddSingleton(new MongoDbMapping()
                .Add<EntityMap>()
                .Add<EscolaridadeMap>()
                .Add<RacaCorMap>()
                .Add<RegiaoMap>()
                .Add<PessoaMap>()
                .Initialize());
        }
        private static IMongoDatabase InitData(this IMongoDatabase mongoDatabase)
        {
            var pessoa = Pessoa.CreateRoot();
            var pessoaCollection = mongoDatabase.GetCollection<Pessoa>();

            if (!pessoaCollection.Find<Pessoa>(Builders<Pessoa>.Filter.Eq(x => x.Id, pessoa.Id)).Any())
                pessoaCollection.InsertOne(pessoa);

            mongoDatabase.GetCollection<Escolaridade>()
               .DeleteMany("{}");
            
            mongoDatabase.GetCollection<RacaCor>()
               .DeleteMany("{}");

            mongoDatabase.GetCollection<Regiao>()
                .DeleteMany("{}");

            mongoDatabase.GetCollection<Escolaridade>()
                .InsertMany(
                new List<Escolaridade>() {
                    new Escolaridade("Ensino Fundamental", "ENFU"),
                    new Escolaridade("Ensino Médio", "ENME"),
                    new Escolaridade("Ensino Superior", "ENSU"),
                    new Escolaridade("Especialização", "ESPE"),
                    new Escolaridade("Mestrado", "MEST"),
                    new Escolaridade("Douturado", "DOUT")
                    }
                );

            mongoDatabase.GetCollection<RacaCor>()
                .InsertMany(
                new List<RacaCor>() {
                    new RacaCor("Branca", "BRA"),
                    new RacaCor("Preta", "PRE"),
                    new RacaCor("Parda", "PAR"),
                    new RacaCor("Amarela", "AMA"),
                    new RacaCor("Indígena", "IND") 
                    }
                );

            mongoDatabase.GetCollection<Regiao>()
                .InsertMany(
                new List<Regiao>() {
                    new Regiao("Região Norte", "NOR"),
                    new Regiao("Região Nordeste", "NRD"),
                    new Regiao("Região Sul", "SUL"),
                    new Regiao("Região Sudeste", "SUD"),
                    new Regiao("Região Centro Oeste", "CTO")
                    }
                );

            return mongoDatabase;
        }
    }
}
