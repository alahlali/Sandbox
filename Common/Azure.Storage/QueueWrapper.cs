using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace Azure.Storage
{
    public class QueueWrapper : IQueueWrapper
    {
        private static QueueClient _queueClient;

        private const string QueueName = "workerqueue";

        public QueueWrapper(string connectionString)
        {
            _queueClient = MessagingFactory.CreateFromConnectionString(connectionString).CreateQueueClient(QueueName);
        }

        public async Task EnqueueTaskMessageAsync(BrokeredMessage message)
        {
            await _queueClient.SendAsync(message);
        }
    }
}