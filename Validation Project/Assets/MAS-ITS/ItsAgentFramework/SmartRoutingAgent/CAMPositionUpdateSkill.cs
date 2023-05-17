using ItsAgentFramework.Communication;
using ItsAgentFramework.Components;
using ItsAgentFramework.SmartRoutingAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ItsAgentFramework.SmartRoutingAgent
{
    public class CAMPositionUpdateSkill : Skill
    {
        private VehicleInfoSkill vehicleInfoSkill;
        private ITSCommunicationSkill iTSCommunicationSkill;

        public CAMPositionUpdateSkill(VehicleInfoSkill vehicleInfoSkill, ITSCommunicationSkill iTSCommunicationSkill) 
        {
            this.vehicleInfoSkill= vehicleInfoSkill;
            this.iTSCommunicationSkill= iTSCommunicationSkill;

            
            int.TryParse(Regex.Match(this.iTSCommunicationSkill!.AgentID, @"\d+").Value, out int matchedIntID);
            var msg = new CAMessage(matchedIntID, Utilities.GetIntID())
            {
                Frequency = 500,
                Characteristics = new CAMessage.StationChar()
                {
                    MobileStation = true
                }
            };
            this.iTSCommunicationSkill.BroadcastedMessages.Add(msg);
        }

        protected override void Act()
        {
            foreach (var message in iTSCommunicationSkill.BroadcastedMessages)
            {
                if (message is not CAMessage camMessage) continue;

                camMessage.Position.Coords.X = (int)vehicleInfoSkill.location.x;
                camMessage.Position.Coords.Y = (int)vehicleInfoSkill.location.y;
            }
        }
    }
}
