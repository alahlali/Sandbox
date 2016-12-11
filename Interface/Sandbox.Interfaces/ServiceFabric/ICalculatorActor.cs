using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Model.Internal;

namespace Sandbox.Interfaces.ServiceFabric
{
    public interface ICalculatorActor : IActor
    {
        Task DoCalculateAsync(TaskRequestMessage message);
        Task<TaskResponse> GetResultAsync();
    }
}
