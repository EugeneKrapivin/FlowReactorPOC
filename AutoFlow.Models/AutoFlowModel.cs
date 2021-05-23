using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoFlow.Api.Models
{
    public class Trigger
    {
        public Trigger(string type, JsonDocument triggerCtx, string fire)
        {
            this.Type = type;
            this.TriggerCtx = triggerCtx;
            this.Fire = fire;
        }

        public string Type { get; }
        public JsonDocument TriggerCtx { get; }
        public string Fire { get; }
    }

    public class Transition
    {
        public Transition(List<string> inputs, string target)
        {
            this.Inputs = inputs;
            this.Target = target;
        }

        public IReadOnlyList<string> Inputs { get; }
        public string Target { get; }
    }

    public class Interceptor
    {
        public Interceptor(string interceptorType, JsonDocument actionContext)
        {
            this.InterceptorType = interceptorType;
            this.ActionContext = actionContext;
        }

        public string InterceptorType { get; }
        public JsonDocument ActionContext { get; }
    }

    public class State
    {
        public State(string value, List<Trigger> triggers, List<Transition> transitions, List<Interceptor> onEntry, List<Interceptor> onTransition)
        {
            this.Value = value;
            this.Triggers = triggers ?? new();
            this.Transitions = transitions ?? new();
            this.OnEntry = onEntry ?? new();
            this.OnTransition = onTransition ?? new();
        }

        public string Value { get; }
        public IReadOnlyList<Trigger> Triggers { get; }
        public IReadOnlyList<Transition> Transitions { get; }
        public IReadOnlyList<Interceptor> OnEntry { get; }
        public IReadOnlyList<Interceptor> OnTransition { get; }
    }

    public class AutoFlowModel
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public IReadOnlyList<State> States { get; }

        public AutoFlowModel(string id, string name, string description, List<State> states)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.States = states;
        }
    }
}
