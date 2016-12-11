using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors;
using System;
using Model.Internal;
using System.Threading.Tasks;
using Sandbox.Interfaces.ServiceFabric;

namespace Gateway.Controllers
{
    [Route("actor")]
    public class ActorController : Controller
    {
        [HttpGet]
        public string Get(int id)
        {
            return "Actor Controller";
        }
        
        [HttpPost("task")]
        public async Task<IActionResult> Post()
        {
            Guid actorId = Guid.NewGuid();

            ICalculatorActor calculatorActor = ActorProxy.Create<ICalculatorActor>(new ActorId(actorId));

            await calculatorActor.DoCalculateAsync(new TaskRequestMessage(actorId));

            return Ok(new { resultId = actorId });
        }

        [HttpGet("task/{guid}")]
        public async Task<IActionResult> Get(string guid)
        {
            ICalculatorActor calculatorActor = ActorProxy.Create<ICalculatorActor>(new ActorId(new Guid(guid)));

            TaskResponse taskResponse = await calculatorActor.GetResultAsync();
            return Ok(taskResponse);
        }
    }
}
