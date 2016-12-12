using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Azure.Storage;
using Microsoft.ServiceBus.Messaging;
using Model.Internal;
using Sandbox.Interfaces.Worker;

namespace WorkerRoleWithSBQueue
{
    internal class WorkerLogic : IWorkerLogic
    {
        private readonly QueueClient _client;
        private readonly IStorageWrapper _storageWrapper;

        public WorkerLogic(IQueueWrapper queueWrapper, IStorageWrapper storageWrapper)
        {
            _client = queueWrapper.GetQueueClient();
            _storageWrapper = storageWrapper;
        }

        public Task<TaskResponse> DoWorkAsync(TaskRequestMessage request)
        {
            return Task.FromResult(new TaskResponse(request.Id, true, new Result(23)));
        }

        public void Loop()
        {
            _client.OnMessage(async (receivedMessage) =>
            {
                try
                {
                    Trace.WriteLine("Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());
                    var result = await DoWorkAsync(receivedMessage.GetBody<TaskRequestMessage>());
                    await _storageWrapper.PutDataAsync(result, "taskresult", result.Id);
                }
                catch
                {
                    Trace.WriteLine("Could not process message: " + receivedMessage.SequenceNumber.ToString());
                }
            });
        }

        public void CloseBusConnection()
        {
            _client.Close();
        }
    }
}
