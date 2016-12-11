using Autofac;
using Azure.Storage;

namespace QueueManagerActor
{
    public class QueueModule : Module
    {
        private readonly string _connectionString;

        public QueueModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QueueWrapper>()
                .WithParameter("connectionString", _connectionString)
                .As<IQueueWrapper>();

            base.Load(builder);
        }
    }
}
