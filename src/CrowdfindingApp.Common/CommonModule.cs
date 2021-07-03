using System.Net.Mail;
using Autofac;
using CrowdfindingApp.Common.Core.Maintainers.CryptoProvider;
using CrowdfindingApp.Common.Core.Maintainers.EmailSender;
using CrowdfindingApp.Common.Core.Maintainers.Hasher;
using CrowdfindingApp.Common.Core.Maintainers.Payment;
using CrowdfindingApp.Common.Core.Maintainers.TokenManager;

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
            builder.RegisterType<RobokassaManager>().AsImplementedInterfaces();
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
