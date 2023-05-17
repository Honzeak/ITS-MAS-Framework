using ItsAgentFramework.Communication;
using ItsAgentFramework.Components;
using ItsAgentFramework.SmartRoutingAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using static ItsAgentFramework.ITSMessage.RefPosition;

namespace ItsAgentFramework.SmartRoutingAgent
{
    public class CongestionMonitorSkill : Skill
    {
        private const double TRAFFIC_LIGHT_MAPPING_RADIUS_SQUARED = 100;
        private VehicleInfoSkill vehicleInfoSkill;
        private ITSCommunicationSkill itsSkill;
        public DENMessage? InterruptedMessage { get; private set; }

        public CongestionMonitorSkill(ITSCommunicationSkill itsSkill, VehicleInfoSkill vehicleInfoSkill)
        {
            this.itsSkill = itsSkill;
            this.itsSkill.DENMReceived += HandleDENM!;
            this.vehicleInfoSkill= vehicleInfoSkill;
        }

        protected override void Act()
        {
        }

        private void HandleDENM(object commSkill, CommunicationEventArgs<DENMessage> cea)
        {
            if (cea is null) return;
            var message = cea.Message;
            if (message is null) return;
            if (!PositionRelevant(message.Position.Coords)) return;
            if (message.Situation.CauseCode == DENMessage.DENCauseCode.TrafficCondition)
            {
                if ( message.Situation.SubCauseCode == 4 && vehicleInfoSkill.UseAltRoute == false)
                {
                    InterruptedMessage = message;
                    base.OnSkillTerminated(SkillTerminatedEventArgs.Reason.Interrupted);
                }
                if (message.Situation.SubCauseCode == 1 && vehicleInfoSkill.UseAltRoute == true)
                {
                    InterruptedMessage = message;
                    base.OnSkillTerminated(SkillTerminatedEventArgs.Reason.Interrupted);
                }
            }
        }

        private bool PositionRelevant(LatLon coords)
        {
            var denmVector = new Vector2(coords.X, coords.Y);
            foreach (var destVector in vehicleInfoSkill.DefaultRouteEndPoints.Select(waypointInfo => waypointInfo._transform.position))
            {
                if (Math.Pow(destVector.x - denmVector.x, 2) + Math.Pow(destVector.y - denmVector.y, 2) <= TRAFFIC_LIGHT_MAPPING_RADIUS_SQUARED)
                    return true;
            }
            return false;
        }

    }
}
