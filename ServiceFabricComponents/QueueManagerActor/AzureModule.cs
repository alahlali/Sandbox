using Autofac;
using Azure.Storage;

namespace QueueManagerActor
{
    public class AzureModule : Module
    {
        private readonly string _queueConnectionString;
        private readonly string _storageConnectionString;

        public AzureModule(string queueConnectionString, string storageConnectionString)
        {
            _queueConnectionString = queueConnectionString;
            _storageConnectionString = storageConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QueueWrapper>()
                .WithParameter("connectionString", _queueConnectionString)
                .As<IQueueWrapper>();

            builder.RegisterType<StorageWrapper>()
                .WithParameter("connectionString", _storageConnectionString)
                .As<IStorageWrapper>();

            base.Load(builder);
        }
    }
}
