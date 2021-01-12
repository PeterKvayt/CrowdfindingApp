using System.Collections.Generic;
using System.Net.Mail;
using System.Reflection;
using Autofac;
using CrowdfindingApp.Common.Localization;
using CrowdfindingApp.Common.Maintainers.CryptoProvider;
using CrowdfindingApp.Common.Maintainers.EmailSender;
using CrowdfindingApp.Common.Maintainers.Hasher;
using CrowdfindingApp.Common.Maintainers.TokenManager;

namespace CrowdfindingApp.Common
{
    public class CommonModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Hasher>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<EmailSender>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CryptoProvider>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<TokenManager>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<SmtpClient>().AsSelf().SingleInstance();

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
