using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Core.Interfaces.Data;
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
        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }

        public IConfiguration Config { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IDataProvider, DataProvider>(options => 
                options.UseSqlServer(Config.GetConnectionString(Configuration.Connection)));

            services.AddAuthentication()
                    .AddHandlers()
                    .AddSwaggerGen()
                    .AddControllers();
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
