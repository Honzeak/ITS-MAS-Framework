using System;
namespace ItsAgentFramework
{
    public abstract class ITSMessage
    {
        public ITSMessage(int messageID)
        {
            Header.MessageID = messageID;
        }

        public MessageHeader Header;

        public RefPosition Position;

        public abstract int GetBroadcastFrequency();

        public struct MessageHeader
        {
            public byte ProtoVersion;
            private int _messageID;
            public int MessageID
            {
                get => _messageID;
                set
                {
                    _messageID = (int)Utilities.ValidateMin(value, 0);
                }
            }
            public TimeSpan Time;
        }
        public struct RefPosition
        {
            public LatLon Coords;
            private int _elevation;
            public int Elevation
            {
                get { return _elevation; }
                set
                {
                    _elevation = (int)Utilities.ValidateMin(value, -10_000);
                }
            }
            public struct LatLon
            {
                // For simulation use-case, coords will be orthogonal
                public int X;
                public int Y;
                public int Latitude { get { return Y; } set { Y = value; } }
                public int Longitude { get { return X; } set { X = value; } }


                // Polar (real-world) implementation

                //public enum Emisphere
                //{
                //    North = 0,
                //    South = 1,
                //    East = 0,
                //    West = 1
                //}
                //private int _degrees;
                //public int Degrees
                //{
                //    get { return _degrees; }
                //    set
                //    {
                //        _degrees = (int)Utilities.ValidateRange(value, 0, 18e8);
                //    }

            }
        }
    }
}
