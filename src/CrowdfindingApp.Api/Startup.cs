using Autofac;
using CrowdfindingApp.Api.Middlewares;
using CrowdfindingApp.Common.Configs;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Core.Services.BackgroundTasks.Filters;
using CrowdfindingApp.Data;
using CrowdfindingApp.Common.Data.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            var config = new AppConfiguration();
            Config.Bind(config);

            services.AddDbContext<IDataProvider, DataProvider>(options => 
                options.UseSqlServer(Config.GetConnectionString(Configuration.Connection)), ServiceLifetime.Scoped);

            services.AddAuthentication(Environment)
                    .ConfigureSwagger()
                    .AddSingleton(config)
                    .AddControllers();

            services.UseHangfire(Config);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.FileStorage(Environment.ContentRootPath, Config);
            builder.RegisterRepositories();
            builder.RegisterModules();
            builder.RegisterResourceProviders();
            builder.RegisterAutoMapper();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionInterceptor>();
            //app.UseDeveloperExceptionPage();

            app.ConfigureStaticFiles(Config);

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

            if(Environment.IsDevelopment())
            {
                app.UseHangfireDashboard();
            }
            else
            {
                app.UseHangfireDashboard(options: new DashboardOptions
                {
                    Authorization = new[] { new AuthorizationFilter() }
                });
            }

        }
    }
}
