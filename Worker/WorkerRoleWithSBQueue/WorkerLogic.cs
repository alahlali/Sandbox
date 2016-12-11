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

        public WorkerLogic(IQueueWrapper queueWrapper)
        {
            _client = queueWrapper.GetQueueClient();
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
                    // Process the message
                    Trace.WriteLine("Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());
                    await DoWorkAsync(receivedMessage.GetBody<TaskRequestMessage>());

                }
                catch
                {
                    Trace.WriteLine("Could not process message: " + receivedMessage.SequenceNumber.ToString());
                }
            });

            //_completedEvent.WaitOne();
        }

        public void CloseBusConnection()
        {
            _client.Close();
        }
    }
}
