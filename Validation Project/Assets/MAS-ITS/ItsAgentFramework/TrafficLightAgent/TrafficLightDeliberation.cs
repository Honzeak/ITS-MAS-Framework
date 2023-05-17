using ItsAgentFramework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ItsAgentFramework.TrafficLightAgent
{
    public class TrafficLightDeliberation : Deliberation<Vector2>
    {
        public TrafficLightDeliberation(Vector2 state, Sequencer sequencer) : base(state, sequencer)
        {
        }

        public override void SetNewGoal(string goal)
        {
            currentGoal= goal;
            _sequencer.AddTasks(ResolveTasksFromGoal(currentGoal));
        }

        protected override void InitFailSafeTask()
        {
            throw new NotImplementedException();
        }

        protected override IList<AgentTask> ResolveTasksFromGoal(string goal)
        {
            return new List<AgentTask>() {new TcBroadcastQueueTask() };
        }
    }
}
