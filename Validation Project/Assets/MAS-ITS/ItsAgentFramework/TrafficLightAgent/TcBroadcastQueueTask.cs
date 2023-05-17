using ItsAgentFramework.Communication;
using ItsAgentFramework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAgentFramework.TrafficLightAgent
{
    public class TcBroadcastQueueTask : AgentTask
    {
        protected override List<(Type, Action<Skill>?)> GetSkillDependencies() => new()
        {
            (typeof(ITSCommunicationSkill), null),
            (typeof(MonitorQueueSkill), null)
        };
    }
}
