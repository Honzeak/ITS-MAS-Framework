using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItsAgentFramework.Components;
using UnityEngine;

namespace ItsAgentFramework.TrafficLightAgent
{
    public class CTrafficLightAgent : Agent<Vector2>
    {
        public CTrafficLightAgent(TrafficLightDeliberation deliberationLayer)
            : base(deliberationLayer)
        {

        }
    }
}
