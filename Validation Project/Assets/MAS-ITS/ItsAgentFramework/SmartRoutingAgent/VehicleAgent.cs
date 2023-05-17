using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ItsAgentFramework.Components;

namespace ItsAgentFramework.SmartRoutingAgent
{
    public class VehicleAgent : Agent<Vector3>
    {
        public VehicleAgent(VehicleDeliberation deliberationLayer) 
            : base(deliberationLayer)
        {

        }
    }
}
