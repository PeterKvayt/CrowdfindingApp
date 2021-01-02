using CrowdfindingApp.Core.Services.User.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace CrowdfindingApp.Core.Services.User
{
    public class UserModule
    {
        public UserModule(IServiceCollection services)
        {
            RegisterHandlers(services);
        }

        private IServiceCollection RegisterHandlers(IServiceCollection services)
        {
            return services.AddSingleton<GetTokenRequestHandler>();
        }
    }
}
