using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sandbox.Interfaces;

using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors;
using System;
using Model.Internal;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [Route("api")]
    public class MainController : Controller
    {
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }
        
        [HttpPost("calculate")]
        public async Task<IActionResult> Post()
        {
            Guid actorId = Guid.NewGuid();
            ICalculatorActor calculatorActor = ActorProxy.Create<ICalculatorActor>(new ActorId(actorId));
            await calculatorActor.DoCalculateAsync();

            return Ok(new { resultId = actorId });
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(string guid)
        {
            ICalculatorActor calculatorActor = ActorProxy.Create<ICalculatorActor>(new ActorId(new Guid(guid)));

            Result result = await calculatorActor.GetResultAsync();
            return Ok(new { result = result.Value });
        }
    }
}
