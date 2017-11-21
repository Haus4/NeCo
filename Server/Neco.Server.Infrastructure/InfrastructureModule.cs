using Autofac;
using Neco.Server.Application.Interfaces;

namespace Neco.Server.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InfrastructureInitializer>().As<IInfrastructureInitializer>().SingleInstance();
            builder.RegisterType<Settings>().As<ISettings>().SingleInstance();
            builder.RegisterType<SocketServer>().AsSelf().As<ISessionManager>().SingleInstance();

            base.Load(builder);
        }
    }
}
