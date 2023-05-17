using Assets.MAS_ITS.ItsAgentFramework.Components;
using ItsAgentFramework.Communication;
using ItsAgentFramework.Components;
using ItsAgentFramework.SmartRoutingAgent;
using ItsAgentFramework.SmartRoutingAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TurnTheGameOn.SimpleTrafficSystem;
using ItsAgentFramework;

namespace Assets.MAS_ITS.ItsAgentFramework.NavigationAgent
{
    public class VehicleAgentWrapper : AgentMonoWrapper<Vector3>
    {
        public int AgentID = Utilities.GetIntID();
        public AITrafficCar AITrafficCar;
        protected override void InitAgent()
        {
            
            var vis = new VehicleInfoSkill(AITrafficCar, AITrafficCar.StartRoute.GetDefaultRouteEndPoints());
            var ics = new ITSCommunicationSkill(AgentID.ToString(), true);
            var dns = new CongestionMonitorSkill(ics, vis);
            var cbs = new CAMPositionUpdateSkill(vis, ics);
            var skillList = new List<Skill>
            {
                vis, ics, dns, cbs
            };
            var s = new VehicleSequencer(skillList);
            var dl = new VehicleDeliberation(gameObject.transform.position , s);
            agent = new VehicleAgent(dl);
            agent.SetNewGoal("dik");
        }
    }
}
