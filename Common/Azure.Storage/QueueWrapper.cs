using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace Azure.Storage
{
    public class QueueWrapper : IQueueWrapper
    {
        private static QueueClient _queueClient;

        private const string QueueName = "workerqueue";

        public QueueWrapper(string connectionString)
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            _queueClient = MessagingFactory.CreateFromConnectionString(connectionString).CreateQueueClient(QueueName);
        }

        public async Task EnqueueTaskMessageAsync(BrokeredMessage message)
        {
            await _queueClient.SendAsync(message);
        }

        public QueueClient GetQueueClient()
        {
            return _queueClient;
        }
    }
}