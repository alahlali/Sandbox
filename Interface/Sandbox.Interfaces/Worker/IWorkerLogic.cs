using System.Threading.Tasks;
using Model.Internal;

namespace Sandbox.Interfaces.Worker
{
    public interface IWorkerLogic
    {
        Task<TaskResponse> DoWorkAsync(TaskRequestMessage request);
        void Loop();
        void CloseBusConnection();
    }
}