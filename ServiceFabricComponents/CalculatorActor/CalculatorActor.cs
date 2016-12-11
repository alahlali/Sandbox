using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Model.Internal;
using Sandbox.Interfaces.ServiceFabric;

namespace CalculatorActor
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class CalculatorActor : Actor, ICalculatorActor
    {
        private const string TaskResponse = "Result";

        public CalculatorActor(ActorService actorService, ActorId actorId) : base(actorService, actorId) { }

        public Task DoCalculateAsync(TaskRequestMessage message)
        {
            StateManager.TryAddStateAsync(TaskResponse, new TaskResponse(message.Id.ToString(), true, new Result(10)));

            return Task.FromResult(false);
        }

        public Task<TaskResponse> GetResultAsync()
        {
            return StateManager.GetStateAsync<TaskResponse>(TaskResponse);
        }
    }
}
