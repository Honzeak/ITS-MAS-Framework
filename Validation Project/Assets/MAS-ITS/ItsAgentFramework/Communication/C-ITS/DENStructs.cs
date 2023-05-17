using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAgentFramework
{
    public partial class DENMessage : ITSMessage
    {
        public struct DENSituation
        {
            public DENCauseCode CauseCode;
            public byte SubCauseCode;
            public SituationSeverity Severity;
            public enum SituationSeverity
            {
                Low = 1,
                Medium = 2,
                High = 3,
                Max = 4
            }
        }

        public struct DENActionID
        {
            private int _stationID;
            public int StationID
            {
                get { return _stationID; }
                set
                {
                    _stationID = (int)Utilities.ValidateMin(value, 0);
                }
            }
            private int _seqNo;
            public int SeqNo
            {
                get { return _seqNo; }
                set
                {
                    _seqNo = (int)Utilities.ValidateMin(value, 0);
                }
            }
            public override string ToString()
            {
                return $"{StationID}.{SeqNo}";
            }
        }

        public enum DENCauseCode
        {
            TrafficCondition = 1,
            Accident = 2,
            Roadworks = 3,
            AdverseWeatherConditionAdhesion = 6,
            HazardousLocationSurfaceCondition = 9,
            HazardousLocationObstacle = 10,
            HazardousLocationAnimal = 11,
            HumanPresenceOnRoad = 12,
            WrongWayDriving = 14,
            RescueAndRecoveryInProgress = 15,
            AdverseWeatherConditionExtreme = 17,
            AdverseWeatherConditionVisibility = 18,
            AdverseWeatherConditionPrecipitation = 19,
            SlowVehicle = 26,
            DangerousEndOfQueue = 27,
            Vehiclebreakdown = 91,
            PostCrash = 92,
            HumanProblem = 93,
            StationaryVehicle = 94,
            EmergencyVehicleApproaching = 95,
            HazardousLocation=96,
            CollisionRisk = 97,
            SignalViolation = 98,
            DangerousSituation = 99
        }

        public struct DENTraceLocationData
        {
            private int _traceID;
            public int TraceID
            {
                get { return _traceID; }
                set
                {
                    _traceID = (int)Utilities.ValidateRange(value, 0, 7);
                }
            }
            public RefPosition Waypoint;
        }
    }
}
