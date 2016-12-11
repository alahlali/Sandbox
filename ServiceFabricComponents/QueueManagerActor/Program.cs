using System;
using System.Threading;
using Framework;

namespace QueueManagerActor
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                ServiceFabricFramework.CreateActorContainerBuilder<QueueManagerActor>()
                    .AddBootstrapper(new QueueManagerBootstrapper("QueueConnectionString", "ConnectionString").Bootstrap)
                    .Register();
                
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ActorEventSource.Current.ActorHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
