using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFlow.Api.Models;
using AutoFlow.Grains;
using Microsoft.Extensions.Logging;
using Orleans;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutoFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchematicsController : ControllerBase
    {
        private readonly IClusterClient _clusterClient;
        private readonly ILogger<SchematicsController> _logger;

        public SchematicsController(IClusterClient clusterClient, ILogger<SchematicsController> logger)
        {
            _clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        // GET api/<FlowMachinesController>/5
        [HttpGet("{id}", Name = "GetAutoFlow")]
        public async Task<AutoFlowModel> Get(string id)
        {
            return await _clusterClient.GetGrain<IAutoFlowMachineGrain>(id).GetSchematic();
        }

        // POST api/<FlowMachinesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AutoFlowModel value)
        {
            var id = Guid.NewGuid().ToString("N");

            await _clusterClient.GetGrain<IAutoFlowMachineGrain>(id).UpdateSchematic(value);

            return CreatedAtRoute("GetAutoFlow", new {id});
        }

        // PUT api/<FlowMachinesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] AutoFlowModel value)
        {
            await _clusterClient.GetGrain<IAutoFlowMachineGrain>(id).UpdateSchematic(value);

            return Ok();
        }

        // DELETE api/<FlowMachinesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _clusterClient.GetGrain<IAutoFlowMachineGrain>(id).DestroySchematic();

            return NoContent();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class Activations1Controller : ControllerBase
    {
        private readonly IClusterClient _clusterClient;
        private readonly ILogger<Activations1Controller> _logger;

        public Activations1Controller(IClusterClient clusterClient, ILogger<Activations1Controller> logger)
        {
            _clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


    }

    [ApiController]
    [Route("api/[controller]")]
    public class Events1Controller : ControllerBase
    {
        private readonly IClusterClient _clusterClient;
        private readonly ILogger<Events1Controller> _logger;

        public Events1Controller(IClusterClient clusterClient, ILogger<Events1Controller> logger)
        {
            _clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
