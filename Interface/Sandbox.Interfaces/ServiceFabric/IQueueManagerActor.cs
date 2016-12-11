using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Model.Internal;

namespace Sandbox.Interfaces.ServiceFabric
{
    public interface IQueueManagerActor : IActor
    {
        Task EnqueueWorkerTaskMessageAsync(TaskRequestMessage message);

        Task<TaskResponse> GetWorkerTaskResultAsync(string requestId);
    }
}
