using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Model.Internal;
using Sandbox.Interfaces.ServiceFabric;

namespace Gateway.Controllers
{
    [Route("worker")]
    public class WorkerRoleController : Controller
    {
        [HttpGet]
        public string Get(int id)
        {
            return "Worker Controller";
        }

        [HttpPost("task")]
        public async Task<IActionResult> Post()
        {
            Guid actorId = Guid.NewGuid();

            IQueueManagerActor queueManagerActor = ActorProxy.Create<IQueueManagerActor>(new ActorId(actorId));

            await queueManagerActor.EnqueueWorkerTaskMessageAsync(new TaskRequestMessage(actorId));

            return Ok(new { resultId = actorId });
        }

        [HttpGet("task/{guid}")]
        public async Task<IActionResult> Get(string guid)
        {
            IQueueManagerActor queueManagerActor = ActorProxy.Create<IQueueManagerActor>(new ActorId(new Guid(guid)));

            TaskResponse taskResponse = await queueManagerActor.GetWorkerTaskResultAsync(guid);

            return Ok(taskResponse);
        }
    }
}
