using CensusApp.Api.Config.AutoMapper;
using CensusApp.Api.Config.MongoDb;
using CensusApp.Api.Core.Domain;
using CensusApp.Api.Core.Infra.Data.MongoDb;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using PainelOuvidoria.Api.Hubs;
using System;
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    string[] origins = { "http://localhost:4200" };
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

            //app.UseHttpsRedirection();

            app.UseWebSockets(new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
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
