using Autofac;
using AutoMapper;
using CrowdfindingApp.Core.Services.Orders.Handlers;
using CrowdfindingApp.Core.Services.Orders.Mappings;

namespace CrowdfindingApp.Core.Services.Orders
{
    public class OrderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repository registration in startup extensions.
            RegisterHandlers(builder);

            builder.RegisterType<OrderProfile>().As<Profile>();
        }

        private void RegisterHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<OrderSearchRequestHandler>().AsSelf();
            builder.RegisterType<SaveOrderRequestHandler>().AsSelf();
            builder.RegisterType<GetUserOrdersRequestHandler>().AsSelf();
            builder.RegisterType<GetProjectOrdersRequestHandler>().AsSelf();
            builder.RegisterType<AcceptPaymentResultRequestHandler>().AsSelf();
        }
    }
}
