using Assets.MAS_ITS.ItsAgentFramework.Components;
using ItsAgentFramework.Communication;
using ItsAgentFramework.Components;
using ItsAgentFramework.TrafficLightAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MAS_ITS.ItsAgentFramework.TrafficLightAgent
{
    public class TrafficLightAgentWrapper : AgentMonoWrapper<Vector2>
    {
        public int AgentID;
        public CarsInArea CarsInArea;
        public int carThreshold = 1;
        protected override void InitAgent()
        {
            var ics = new ITSCommunicationSkill(AgentID.ToString(), true);
            var location = new Vector2(gameObject.transform.position.x,
                gameObject.transform.position.y);
            var mqs = new MonitorQueueSkill(ics, location, CarsInArea, carThreshold);
            var skillList = new List<Skill> { ics,mqs };
            var sq = new TrafficLightSequencer(skillList);
            var dl = new TrafficLightDeliberation(location, sq);
            agent = new CTrafficLightAgent(dl);
            // Goal can be anything because the agent is configured for a single goal
            agent.SetNewGoal("anything");
        }
    }
}
