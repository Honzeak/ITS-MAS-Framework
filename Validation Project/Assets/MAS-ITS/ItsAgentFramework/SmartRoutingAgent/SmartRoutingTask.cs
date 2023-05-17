using ItsAgentFramework.Communication;
using ItsAgentFramework.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ItsAgentFramework.SmartRoutingAgent
{
    public class SmartRoutingTask : AgentTask
    {
        private bool useAltRoute;
        public SmartRoutingTask(bool useAlternativeRoute = false)
        {
            useAltRoute = useAlternativeRoute;
            DependentSkills = GetSkillDependencies();
        }

        protected override List<(Type, Action<Skill>?)> GetSkillDependencies() => new()
            {
                (typeof(VehicleInfoSkill), useAltRoute ? UseAlternativeRoute : NotUseAlternativeRoute),
                (typeof(CongestionMonitorSkill), null),
                (typeof(ITSCommunicationSkill), null),
                (typeof(CAMPositionUpdateSkill), null)
            };


        private void UseAlternativeRoute(Skill skill)
        {
            if (skill is not VehicleInfoSkill vehicleInfoSkill) return;
            vehicleInfoSkill.SetUseAltRoute(true);
        }
        private void NotUseAlternativeRoute(Skill skill)
        {
            if (skill is not VehicleInfoSkill vehicleInfoSkill) return;
            vehicleInfoSkill.SetUseAltRoute(false);
        }
    }
}
