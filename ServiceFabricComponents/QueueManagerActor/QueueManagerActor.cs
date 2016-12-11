using System.Threading.Tasks;
using Azure.Storage;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Model.Internal;
using Sandbox.Interfaces.ServiceFabric;

namespace QueueManagerActor
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class QueueManagerActor : Actor, IQueueManagerActor
    {
        private readonly IQueueWrapper _queueWrapper;

        public QueueManagerActor(ActorService actorService, ActorId actorId, IQueueWrapper queueWrapper)
            : base(actorService, actorId)
        {
            _queueWrapper = queueWrapper;
        }

        public async Task EnqueueWorkerTaskMessageAsync(TaskRequestMessage requetsMessage)
        {
            var message = new BrokeredMessage(requetsMessage);

            await _queueWrapper.EnqueueTaskMessageAsync(message);            
        }

        public Task<TaskResponse> GetWorkerTaskResultAsync(string requestId)
        {
            throw new System.NotImplementedException();
        }
    }
}
