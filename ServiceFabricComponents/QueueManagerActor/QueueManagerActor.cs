using System;
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
        private readonly IStorageWrapper _storageWrapper;

        public QueueManagerActor(ActorService actorService, ActorId actorId, IQueueWrapper queueWrapper, IStorageWrapper storageWrapper)
            : base(actorService, actorId)
        {
            _queueWrapper = queueWrapper;
            _storageWrapper = storageWrapper;
        }

        public async Task EnqueueWorkerTaskMessageAsync(TaskRequestMessage requetsMessage)
        {
            var message = new BrokeredMessage(requetsMessage);

            await _queueWrapper.EnqueueTaskMessageAsync(message);            
        }

        public Task<TaskResponse> GetWorkerTaskResultAsync(string requestId)
        {
            // check if data exists .. ca reste artisanale tout ca .. x) 
            return _storageWrapper.GetDataAsync<TaskResponse>(new Guid(requestId), "taskresult");
        }
    }
}
