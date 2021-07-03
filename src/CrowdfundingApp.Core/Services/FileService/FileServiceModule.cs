using Autofac;
using CrowdfundingApp.Core.Services.FileService.Handlers;

namespace CrowdfundingApp.Core.Services.FileService
{
    public class FileServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SaveImageRequestHandler>().AsSelf();

        }
    }
}
