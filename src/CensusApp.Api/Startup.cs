using CensusApp.Api.Config.MongoDb;
using CensusApp.Api.Core.Domain._Base;
using CensusApp.Api.Core.Infra.Data.MongoDb;
using CensusApp.Api.Core.Infra.Data.MongoDb.Maps;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization;
using System.Reflection;

namespace CensusApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMongoDb(Configuration);
            services.AddSingleton<MongoDbMapping>(new MongoDbMapping()
                .Add<EntityMap>()
                .Add<EscolaridadeMap>()
                .Add<RacaCorMap>()
                .Add<PessoaMap>()
                .Initialize()
                );
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IRepository<>), typeof(MongoDbRepository<>));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CensusApp", Version = "v1" });
            });



        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CensusApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck");
            });
        }
    }
}
