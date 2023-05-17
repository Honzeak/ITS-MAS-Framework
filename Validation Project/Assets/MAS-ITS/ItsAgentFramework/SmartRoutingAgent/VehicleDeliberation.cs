using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using ItsAgentFramework.Components;
using ItsAgentFramework.SmartRoutingAgent;

namespace ItsAgentFramework.SmartRoutingAgent
{
    public class VehicleDeliberation : Deliberation<Vector3>
    {
        private Vector3 _destination;
        private VehicleSequencer _navigationSequencer => (VehicleSequencer) _sequencer;


        public VehicleDeliberation(Vector3 location, VehicleSequencer sequencer) : base(location, sequencer)
        {
        }

        protected override void HandleTaskFailed(object sequencer, TaskTerminatedEventArgs ttea)
        {
            var failedSkill = ttea.TerminatedSkill;
            if (failedSkill is CongestionMonitorSkill dnSkill)
            {
                if (dnSkill.InterruptedMessage != null &&
                    dnSkill.InterruptedMessage.Situation.CauseCode == DENMessage.DENCauseCode.TrafficCondition)
                {
                    if (dnSkill.InterruptedMessage.Situation.SubCauseCode == 4)
                        SetNewGoal("previousFailed", TaskOverrideOptions.UseAlternativeRoute);
                    else
                        SetNewGoal("previousFailed", TaskOverrideOptions.NoOverride);
                }
                return;
            }
            base.HandleTaskFailed(sequencer, ttea);
        }

        public void SetNewGoal(string destinationString, TaskOverrideOptions options)
        {
            var tasks = ResolveTasksFromGoal(destinationString, options);

            _sequencer.AddTasks(tasks, Sequencer.AddTaskOptions.Overwrite);
            _sequencer.Start();
        }

        public override void SetNewGoal(string destinationString)
        {
            var tasks = ResolveTasksFromGoal(destinationString);

            _sequencer.AddTasks(tasks, Sequencer.AddTaskOptions.Overwrite);
            _sequencer.Start();
        }

        private IEnumerable<AgentTask> GetTasks(IEnumerable<Vector2> shortestPath)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Vector2> ComputeShortestPath(Vector2 destinationVector)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<AgentTask> PathToTasks(IEnumerable<Vector2> path)
        {
            throw new NotImplementedException();
        }

        private Vector2 ResolveDestination(string stringDestination) => stringDestination.Trim().ToLower() switch
        {
            "pub" => new Vector2(2, 3),
            "school" => new Vector2(4, 2),
            _ => throw new ArgumentOutOfRangeException(nameof(stringDestination), $"Not expected direction value: {stringDestination}"),
        };

        protected override void InitFailSafeTask()
        {
            throw new NotImplementedException();
        }

        protected IList<AgentTask> ResolveTasksFromGoal(string goal, TaskOverrideOptions options = TaskOverrideOptions.NoOverride)
        {
            if(options == TaskOverrideOptions.UseAlternativeRoute)
            {
                return new List<AgentTask>() { new SmartRoutingTask(true)};
            }
            return new List<AgentTask>() { new SmartRoutingTask() };
        }

        protected override IList<AgentTask> ResolveTasksFromGoal(string goal)
        {
            throw new NotImplementedException();
        }

        public enum TaskOverrideOptions
        {
            NoOverride,
            UseAlternativeRoute
        }
    }

}
