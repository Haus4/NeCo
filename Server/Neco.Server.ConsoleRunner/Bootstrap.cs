using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Neco.Server.Infrastructure;
using Neco.Server.Application;
using Neco.Server.Application.Interfaces;

namespace Neco.Server.ConsoleRunner
{
    public static class Bootstrap
    {
        private IInfrastructureInitializer infrastructureInitilizer;
        private static bool _isRunning = false;

        public static IContainer Run()
        {
            if (_isRunning)
                throw new InvalidOperationException();
            _isRunning = true;

            var builder = new ContainerBuilder();
            builder.RegisterModule<InfrastructureModule>();

            var container = builder.Build();
            ServiceLocator.Init(container);
            container.Resolve<IInfrastructureInitializer>().Init();

            return container;
        }
    }
}
