﻿using System.Collections.Generic;
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
            builder.RegisterType<Hasher>().AsImplementedInterfaces();
            builder.RegisterType<EmailSender>().AsImplementedInterfaces();
            builder.RegisterType<CryptoProvider>().AsImplementedInterfaces();
            builder.RegisterType<TokenManager>().AsImplementedInterfaces();

            builder.RegisterType<ResourceProvider>()
                .WithParameter((x, _) => x.Name == "stringResources", (_, __) => new List<(string, Assembly)>()
                {
                    ("CrowdfindingApp.Common.Resources.EmailMessages", Assembly.GetExecutingAssembly())
                })
                .As<IResourceProvider>()
                .SingleInstance();
        }
    }
}