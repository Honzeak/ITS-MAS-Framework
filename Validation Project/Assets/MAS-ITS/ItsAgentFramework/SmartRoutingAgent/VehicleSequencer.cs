using ItsAgentFramework.Components;
using ItsAgentFramework.SmartRoutingAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAgentFramework.SmartRoutingAgent
{
    public class VehicleSequencer : Sequencer
    {
        public VehicleSequencer(List<Skill> skillList) : base(skillList)
        {
        }

        protected override void OnSkillInterrupted(Skill skill)
        {
            if (skill is CongestionMonitorSkill)
            {
                base.OnTaskFailed(skill);
            }
        }
    }
}
