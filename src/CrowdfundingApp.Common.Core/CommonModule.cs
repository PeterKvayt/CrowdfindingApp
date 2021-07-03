using System.Net.Mail;
using Autofac;
using CrowdfundingApp.Common.Core.Maintainers.CryptoProvider;
using CrowdfundingApp.Common.Core.Maintainers.EmailSender;
using CrowdfundingApp.Common.Core.Maintainers.Hasher;
using CrowdfundingApp.Common.Core.Maintainers.Payment;
using CrowdfundingApp.Common.Core.Maintainers.TokenManager;

namespace CrowdfundingApp.Common.Core
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
            //        ("CrowdfundingApp.Common.Resources.EmailResources", typeof(CommonModule).Assembly)
            //    }).Named<List<(string, Assembly)>>("resources");

            //builder.RegisterType<ResourceProvider>()
            //    .WithParameter((x, _) => x.Name == "stringResources", (_, __) => new List<(string, Assembly)>()
            //    {
            //        ("CrowdfundingApp.Common.Resources.EmailResources", typeof(CommonModule).Assembly)
            //    })
            //    .Named<IResourceProvider>("resources")
            //    .SingleInstance();
        }
    }
}
