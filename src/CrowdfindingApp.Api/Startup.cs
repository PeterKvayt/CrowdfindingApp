using Autofac;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Core.Interfaces.Data;
using CrowdfindingApp.Core.Services.User;
using CrowdfindingApp.Data;
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

            Config = builder.Build();
        }

        public IConfigurationRoot Config { get; private set; }

        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddDbContext<IDataProvider, DataProvider>(options => 
                options.UseSqlServer(Config.GetConnectionString(Configuration.Connection)));

            services.AddAuthentication(env)
                    .AddSwaggerGen()
                    .AddControllers();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<UserModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger()
               .UseSwaggerUI(config =>
               {
                   config.SwaggerEndpoint("/swagger/v1/swagger.json", "Api");
                   config.RoutePrefix = string.Empty;
               });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
