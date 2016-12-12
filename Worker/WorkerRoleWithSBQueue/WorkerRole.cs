using System.Diagnostics;
using System.Threading;
using Autofac;
using Azure.Storage;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Sandbox.Interfaces.Worker;

namespace WorkerRoleWithSBQueue
{
    public class WorkerRole : RoleEntryPoint
    {
        public IWorkerLogic WorkerLogic;

        private readonly ManualResetEvent _completedEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.WriteLine("Starting processing of messages");
            WorkerLogic.Loop();
        }

        public override bool OnStart()
        {
            ConfigureContainer();

            return base.OnStart();
        }

        public override void OnStop()
        {
            WorkerLogic.CloseBusConnection();
            _completedEvent.Set();
            base.OnStop();
        }

        private void ConfigureContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<QueueWrapper>()
                .WithParameter("connectionString", CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString"))
                .As<IQueueWrapper>();

            containerBuilder.RegisterType<StorageWrapper>()
                .WithParameter("connectionString", CloudConfigurationManager.GetSetting("Microsoft.Storage.ConnectionString"))
                .As<IStorageWrapper>();

            containerBuilder.RegisterType<WorkerLogic>().As<IWorkerLogic>();

            var container = containerBuilder.Build();

            WorkerLogic = container.Resolve<IWorkerLogic>();
        }
    }
}
