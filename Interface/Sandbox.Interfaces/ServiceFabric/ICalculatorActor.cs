using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Model.Internal;

namespace Sandbox.Interfaces
{
    public interface ICalculatorActor : IActor
    {
        Task DoCalculateAsync();
        Task<Result> GetResultAsync();
    }
}
