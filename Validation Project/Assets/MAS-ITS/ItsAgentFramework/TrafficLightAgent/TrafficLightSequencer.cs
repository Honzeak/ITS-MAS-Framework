using ItsAgentFramework;
using ItsAgentFramework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.MAS_ITS.ItsAgentFramework.TrafficLightAgent
{
    internal class TrafficLightSequencer : Sequencer
    {
        public TrafficLightSequencer(List<Skill> skillList) : base(skillList)
        {
        }

        protected override void OnSkillInterrupted(Skill skill)
        {
            throw new NotImplementedException();
        }
    }
}
