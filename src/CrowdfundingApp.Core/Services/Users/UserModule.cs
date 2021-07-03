using Autofac;
using AutoMapper;
using CrowdfundingApp.Core.Services.Users.Handlers;
using CrowdfundingApp.Core.Services.Users.Mappings;

namespace CrowdfundingApp.Core.Services.Users
{
    public class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterHandlers(builder);

            builder.RegisterType<UserProfile>().As<Profile>();
            // Repository registration in startup extensions.


            //builder.RegisterType<ResourceProvider>()
            //    .WithParameter((x, _) => x.Name == "stringResources", (_, __) => new List<(string, Assembly)>()
            //    {
            //        ("CrowdfundingApp.Core.Services.User.Resources.ActionMessages", typeof(UserModule).Assembly)
            //    })
            //    .AsImplementedInterfaces()
            //    ;
        }

        private void RegisterHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterRequestHandler>().AsSelf();
            builder.RegisterType<GetTokenRequestHandler>().AsSelf();
            builder.RegisterType<ForgotPasswordRequestHandler>().AsSelf();
            builder.RegisterType<ResetPasswordRequestHandler>().AsSelf();
            builder.RegisterType<GetUserInfoRequestHandler>().AsSelf();
            builder.RegisterType<UpdateUserRequestHandler>().AsSelf();
            builder.RegisterType<ChangePasswordRequestHandler>().AsSelf();
            builder.RegisterType<GetUserInfoByIdRequestHandler>().AsSelf();
            builder.RegisterType<EditUserRoleRequestHandler>().AsSelf();
        }
    }
}
