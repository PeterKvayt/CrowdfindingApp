using Autofac;
using AutoMapper;
using CrowdfindingApp.Core.Services.Rewards.Handlers;
using CrowdfindingApp.Core.Services.Rewards.Mappings;

namespace CrowdfindingApp.Core.Services.Rewards
{
    public class RewardsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repository registration in startup extensions.
            RegisterHandlers(builder);

            builder.RegisterType<RewardProfile>().As<Profile>();
        }
        private void RegisterHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<GetPublicRewardsByProjectIdRequestHandler>().AsSelf();
            builder.RegisterType<GetPublicRewardByIdRequestHandler>().AsSelf();

        }
    }
}
