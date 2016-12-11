using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace Azure.Storage
{
    public interface IQueueWrapper
    {
        Task EnqueueTaskMessageAsync(BrokeredMessage message);
    }
}
