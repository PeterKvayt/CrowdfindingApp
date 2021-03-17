using System.Net.Mail;
using Autofac;
using CrowdfindingApp.Common.Maintainers.CryptoProvider;
using CrowdfindingApp.Common.Maintainers.EmailSender;
using CrowdfindingApp.Common.Maintainers.Hasher;
using CrowdfindingApp.Common.Maintainers.TokenManager;

namespace CrowdfindingApp.Common
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Hasher>().AsImplementedInterfaces();
            builder.RegisterType<EmailSender>().AsImplementedInterfaces();
            builder.RegisterType<CryptoProvider>().AsImplementedInterfaces();
            builder.RegisterType<TokenManager>().AsImplementedInterfaces();
            builder.RegisterType<SmtpClient>().AsSelf();

            //builder.RegisterInstance(new List<(string, Assembly)>()
            //    {
            //        ("CrowdfindingApp.Common.Resources.EmailResources", typeof(CommonModule).Assembly)
            //    }).Named<List<(string, Assembly)>>("resources");

            //builder.RegisterType<ResourceProvider>()
            //    .WithParameter((x, _) => x.Name == "stringResources", (_, __) => new List<(string, Assembly)>()
            //    {
            //        ("CrowdfindingApp.Common.Resources.EmailResources", typeof(CommonModule).Assembly)
            //    })
            //    .Named<IResourceProvider>("resources")
            //    .SingleInstance();
        }
    }
}
