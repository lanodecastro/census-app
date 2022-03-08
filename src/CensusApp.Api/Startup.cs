using CensusApp.Api.Config.AutoMapper;
using CensusApp.Api.Config.MongoDb;
using CensusApp.Api.Core.Domain;
using CensusApp.Api.Core.Domain.Commands;
using CensusApp.Api.Core.Domain.Commands.CriarPessoa;
using CensusApp.Api.Core.Infra.Data.MongoDb;
using CensusApp.Api.Hubs;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Reflection;

namespace CensusApp.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    string[] origins = { "http://localhost:4200;http://localhost" };
                    builder
                        .WithOrigins(origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed((host) => true);
                });
            });
            services.AddSignalR();
            services.AddMongoDb(Configuration);
            services.AddSingleton(AutoMapperConfig.InitializeMapper());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IRepository<>), typeof(MongoDbRepository<>));

            services.AddScoped<IPipelineBehavior<CriarPessoaRequest, ICommandResponse>, ValidarPessoaRequestHandler>();
            services.AddScoped<IPipelineBehavior<CriarPessoaRequest, ICommandResponse>, BuscarReferenciaHandler>();
            services.AddScoped<IPipelineBehavior<CriarPessoaRequest, ICommandResponse>, VincularFiliacao>();
            services.AddScoped<IPipelineBehavior<CriarPessoaRequest, ICommandResponse>, CriarPessoaHandler>();
            services.AddScoped<IPipelineBehavior<CriarPessoaRequest, ICommandResponse>, NotificarPessoaCriadaHandler>();


            services.AddScoped<RequestContext>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CensusApp", Version = "v1" });
            });


        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMongoDatabase mongoDatabase)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CensusApp v1"));
            }

            app.UseWebSockets(new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120)
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck");
                endpoints.MapHub<RealtimeMessages>("/real-time");
            });
        }
    }
}
