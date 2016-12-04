using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Sandbox.Interfaces;
using Model.Internal;

namespace CalculatorActor
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class CalculatorActor : Actor, ICalculatorActor
    {
        private const string RESULT = "Result";

        public CalculatorActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public Task DoCalculateAsync()
        {
            StateManager.TryAddStateAsync(RESULT, new Result(10));

            return Task.FromResult(false);
        }

        public Task<Result> GetResultAsync()
        {
            return StateManager.GetStateAsync<Result>(RESULT);
        }
    }
}
