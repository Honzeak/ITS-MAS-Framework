namespace ItsAgentFramework
{
    public class CAMessage : ITSMessage
    {
        private int _stationID;
        public int StationID
        { get { return _stationID; }
            set
            {
                _stationID = (int)Utilities.ValidateMin(value, 0);
            }
        }
        private int _frequency;
        public int Frequency
        {
            get { return _frequency; }
            set
            {
                _frequency = (int)Utilities.ValidateRange(value, 100, 1000);
            }
        }

        public StationChar Characteristics;
        
        public struct StationChar
        {
            public bool MobileStation;
            public bool PrivateStation;
            public bool PhysicalStation;
        }

        public CAMessage(int stationID, int messageID) : base(messageID)
        {
            StationID = stationID;
        }

        public override int GetBroadcastFrequency()
        {
            return (Frequency is not default(int)) ? Frequency : 1000;
        }
    }
}
