using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Neco.Infrastructure.Protocol;
using log4net;
using Neco.Server.Application;
using System.Reflection;
using Neco.DataTransferObjects;

namespace Neco.Server.Infrastructure.Commands
{
    public class RequestCommand : NecoCommandBase
    {
        static RequestCommand()
        {
            try
            {
                var servicesTypes = typeof(ChatService).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(BaseService)));
                var servicesInstances = new List<BaseService>();
                var methods = new List<Tuple<MethodInfo, BaseService>>();

                foreach (var serviceType in servicesTypes)
                {
                    var controller = ServiceLocator.Resolve(serviceType) as BaseService;
                    servicesInstances.Add(controller);
                    methods.AddRange(serviceType.GetMethods().Select(i => new Tuple<MethodInfo, BaseService>(i, controller)));
                }

                foreach (var method in methods)
                {
                    var parameters = method.Item1.GetParameters();
                    if (parameters.Length != 2)
                        continue;
                    AppServicesMethods[parameters[1].ParameterType] = new Tuple<MethodInfo, BaseService>(method.Item1, method.Item2);
                }
            }
            catch (Exception exc)
            {
                log.Error(exc.Message);
                throw;
            }
        }

        protected static readonly ILog log = LogManager.GetLogger(typeof(RequestCommand));

        private static readonly Dictionary<Type, Tuple<MethodInfo, BaseService>> AppServicesMethods =
            new Dictionary<Type, Tuple<MethodInfo, BaseService>>();

        public override async void ExecuteExternalCommand(ClientSession session, byte[] data)
        {
            var request = RequestSerializer.Deserialize<RequestBase>(data);
            if (!AppServicesMethods.ContainsKey(request.GetType())) return;
            var methodAndInstancePair = AppServicesMethods[request.GetType()];

            log.InfoFormat("{0}  {1}", methodAndInstancePair.Item1.Name, request.GetType().Name);

            try
            {
                var responseObj = methodAndInstancePair.Item1.Invoke(methodAndInstancePair.Item2, new object[] { session, request });

                if (methodAndInstancePair.Item1.ReturnType.IsSubclassOf(typeof(Task)))
                {
                    var value = await(dynamic)responseObj;
                    session.Send(value);
                }
                else if (methodAndInstancePair.Item1.ReturnType != typeof(void))
                {
                    session.Send(responseObj);
                }
            }
            catch (Exception exc)
            {
                log.Error(exc);
                log.ErrorFormat("{0}"+Environment.NewLine+
                    "{1} failed ({2},  {3})", exc.Message, methodAndInstancePair.Item1.Name, request.GetType().Name, session.SessionID);
            }
        }

        protected override CommandTypes CommandType
        {
            get { return CommandTypes.Request; }
        }
    }
}
