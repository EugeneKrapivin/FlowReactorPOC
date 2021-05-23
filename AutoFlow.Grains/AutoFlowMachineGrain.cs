using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using AutoFlow.Api.Models;
using Orleans;
using Orleans.Runtime;

namespace AutoFlow.Grains
{
    public interface IAutoFlowMachineGrain : IGrainWithStringKey
    {
        ValueTask<AutoFlowModel> GetSchematic();

        Task UpdateSchematic(AutoFlowModel schematic);

        Task DestroySchematic();
    }

    public class FlowSchematicState
    {
        public AutoFlowModel Schematic { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

    public class AutoFlowMachineGrain : Grain, IAutoFlowMachineGrain
    {
        private readonly IPersistentState<FlowSchematicState> _schematics;

        public AutoFlowMachineGrain([PersistentState("schematics", "schematics-storage")] IPersistentState<FlowSchematicState> schematics)
        {
            _schematics = schematics;
        }
        
        public ValueTask<AutoFlowModel> GetSchematic()
        {
            return ValueTask.FromResult(_schematics.State.Schematic);
        }

        public async Task UpdateSchematic(AutoFlowModel schematics)
        {
            if (schematics is null) throw new ArgumentNullException(nameof(schematics));

            _schematics.State.Schematic = schematics;
            _schematics.State.UpdatedAt = DateTime.UtcNow;
            _schematics.State.CreatedAt ??= _schematics.State.UpdatedAt;

            await _schematics.WriteStateAsync();
        }

        public async Task DestroySchematic()
        {
            await _schematics.ClearStateAsync();
            
            this.DeactivateOnIdle();
        }
    }
}
