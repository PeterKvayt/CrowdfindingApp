using Autofac;
using CrowdfindingApp.Core.Services.FileService.Handlers;

namespace CrowdfindingApp.Core.Services.FileService
{
    public class FileServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SaveImageRequestHandler>().AsSelf();

        }
    }
}
