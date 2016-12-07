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
        private const string Result = "Result";

        public CalculatorActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public Task DoCalculateAsync()
        {
            StateManager.TryAddStateAsync(Result, new Result(10));

            return Task.FromResult(false);
        }

        public Task<Result> GetResultAsync()
        {
            return StateManager.GetStateAsync<Result>(Result);
        }
    }
}
