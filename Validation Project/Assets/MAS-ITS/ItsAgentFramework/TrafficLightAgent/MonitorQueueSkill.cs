using ItsAgentFramework.Communication;
using ItsAgentFramework.Components;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace ItsAgentFramework.TrafficLightAgent
{
    public class MonitorQueueSkill : Skill
    {
        private const byte TRAFFIC_JAM_SCC = 4;
        private const byte FREE_FLOW_SCC = 1;

        private ITSCommunicationSkill itsSkill;
        private Vector2 selfLocation;
        private int sectorThreshold = 0;
        private int agentsWithin => sectorMonitor.noCars;
        private DENMessage publishedMessage;
        private CarsInArea sectorMonitor;
        private int currentSubCauseCode => publishedMessage.Situation.SubCauseCode;


        public MonitorQueueSkill(ITSCommunicationSkill itsSkill, Vector2 selfLocation, CarsInArea  sectorMonitor, int sectorThreshold) 
        { 
            this.itsSkill= itsSkill;
            this.selfLocation = selfLocation;
            this.itsSkill.CAMReceived += HandleCAM!;
            this.sectorThreshold = sectorThreshold;
            this.sectorMonitor = sectorMonitor;

            var message = GenerateDENM(FREE_FLOW_SCC);
            publishedMessage = message;
            this.itsSkill.BroadcastedMessages.Add(publishedMessage);
        }
        protected override void Act()
        {
            UpdateBroadcast();
        }

        private DENMessage GenerateDENM(byte subCauseCode)
        {
            return new DENMessage(Utilities.GetIntID())
            {
                Frequency = 255,
                Situation = new DENMessage.DENSituation
                {
                    CauseCode = DENMessage.DENCauseCode.TrafficCondition,
                    SubCauseCode = subCauseCode // 1 for no jam, 4 for jam
                },
                Position = new ITSMessage.RefPosition
                {
                    Coords = new ITSMessage.RefPosition.LatLon
                    {
                        X = (int)selfLocation.x,
                        Y = (int)selfLocation.y
                    }
                }
            };
        }

        private void UpdateTrafficSituation(byte subCauseCode)
        {
            publishedMessage.Situation.SubCauseCode = subCauseCode;
        }

        protected void HandleCAM(object commSkill, CommunicationEventArgs<CAMessage> cea)
        {
            var loc = (cea.Message.Position.Coords.X, cea.Message.Position.Coords.Y);
            var agentID = cea.Message.StationID;
        }

        private void UpdateBroadcast()
        {
            if (currentSubCauseCode != TRAFFIC_JAM_SCC && agentsWithin >= sectorThreshold)
            {
                Debug.Log("Queue status changed to full");
                UpdateTrafficSituation(TRAFFIC_JAM_SCC);
            }
            if (currentSubCauseCode != FREE_FLOW_SCC && agentsWithin < sectorThreshold)
            {
                UpdateTrafficSituation(FREE_FLOW_SCC);
            }
        }

        private bool IsWithinSector((int X, int Y) loc)
        {
            return true;
        }
    }
}