using Autofac;
using CrowdfindingApp.Api.Middlewares;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Core.Interfaces.Data;
using CrowdfindingApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrowdfindingApp.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Environment = env;

            Config = builder.Build();
        }

        public IConfigurationRoot Config { get; private set; }

        public ILifetimeScope AutofacContainer { get; private set; }

        public IWebHostEnvironment Environment { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IDataProvider, DataProvider>(options => 
                options.UseSqlServer(Config.GetConnectionString(Configuration.Connection)));

            services.AddAuthentication(Environment)
                    .ConfigureSwagger()
                    .AddSingleton(Config)
                    .AddControllers();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterRepositories();
            builder.RegisterModules();
            builder.RegisterResourceProviders();
            builder.RegisterAutoMapper();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionInterceptor>();

            //app.UseDeveloperExceptionPage();

            app.UseCors(config => 
                config.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSwagger()
                .UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "Api");
                    config.RoutePrefix = string.Empty;
                })
                .UseRouting()
                .UseMiddleware<AuthTokenInterceptor>();

            app.UseAuthentication()
                .UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
