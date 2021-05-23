using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace AutoFlow.Grains
{

    public interface IFlowActivationGrain : IGrainWithStringKey
    {
        Task CreateFromSchematics(string schematicId);
    }

    public class FlowActivationGrain : Grain, IFlowActivationGrain
    {
        public async Task CreateFromSchematics(string schematicId)
        {
            var schematics = this.GrainFactory.GetGrain<IAutoFlowMachineGrain>(schematicId).GetFlowModel();

        }
    }
}
