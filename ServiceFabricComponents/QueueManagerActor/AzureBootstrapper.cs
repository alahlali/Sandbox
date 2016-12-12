using System.Fabric;
using Autofac;

namespace QueueManagerActor
{
    public class AzureBootstrapper
    {
        private const string Config = "Config";
        private readonly string _queue_section = "QueueConnectionString";
        private readonly string _storage_section = "StorageConnectionString";
        private readonly string _parameter = "ConnectionString";


        public void Bootstrap(ContainerBuilder containerbuilder, StatefulServiceContext context)
        {
            var configurationPackage = context.CodePackageActivationContext.GetConfigurationPackageObject(Config);
            var queueConnectionString = configurationPackage.Settings.Sections[_queue_section].Parameters[_parameter].Value;
            var storageConnectionString = configurationPackage.Settings.Sections[_storage_section].Parameters[_parameter].Value;

            containerbuilder.RegisterModule(new AzureModule(queueConnectionString, storageConnectionString));
        }
    }
}
