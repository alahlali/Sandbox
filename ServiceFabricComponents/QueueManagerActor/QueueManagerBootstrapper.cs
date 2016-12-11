using System.Fabric;
using Autofac;

namespace QueueManagerActor
{
    public class QueueManagerBootstrapper
    {
        private const string Config = "Config";
        private readonly string _section;
        private readonly string _parameter;

        public QueueManagerBootstrapper(string section, string parameter)
        {
            _section = section;
            _parameter = parameter;
        }

        public void Bootstrap(ContainerBuilder containerbuilder, StatefulServiceContext context)
        {
            var configurationPackage = context.CodePackageActivationContext.GetConfigurationPackageObject(Config);

            var connectionStringParameter = configurationPackage.Settings.Sections[_section].Parameters[_parameter].Value;

            containerbuilder.RegisterModule(new QueueModule(connectionStringParameter));
        }
    }
}
