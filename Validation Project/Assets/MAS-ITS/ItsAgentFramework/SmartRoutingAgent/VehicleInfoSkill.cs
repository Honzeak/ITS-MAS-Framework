using ItsAgentFramework.Components;
using UnityEngine;
using TurnTheGameOn.SimpleTrafficSystem;
using System.Collections.Generic;

namespace ItsAgentFramework.SmartRoutingAgent
{
    public class VehicleInfoSkill : Skill 
    {
        private AITrafficCar carInfo;
        public bool UseAltRoute => carInfo.UseAltRoute;
        public Vector3 DestinaitonVector;
        public Vector2 location;
        public List<CarAIWaypointInfo> DefaultRouteEndPoints;

        public VehicleInfoSkill(AITrafficCar CarInfo, List<CarAIWaypointInfo> defaultRouteEndPoints = null)
        {
            carInfo = CarInfo;
            DefaultRouteEndPoints = defaultRouteEndPoints;
        }


        protected override void Act()
        {
            DestinaitonVector = carInfo.CurrentDestination;
            location = carInfo.gameObject.transform.position;
        }

        public void SetUseAltRoute(bool use)
        {
            carInfo.UseAltRoute = use;
        }

        
    }
}