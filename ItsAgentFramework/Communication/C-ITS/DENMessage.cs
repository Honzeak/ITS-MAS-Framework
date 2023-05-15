using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAgentFramework
{
    public partial class DENMessage : ITSMessage
    {
        public DENActionID ActionID;
        public byte DataVersion;
        public TimeSpan ExpirityTime;
        public byte Frequency;
        private int _reliability;
        public bool IsNegation;
        public DENSituation Situation;
        public DENTraceLocationData[] Trace;
        public int Reliability
        {
            get { return _reliability; }
            set
            {
                _reliability = (int)Utilities.ValidateRange(value, 0, 255);
            }
        }


        public DENMessage(int messageID) : base(messageID) { }

        public override int GetBroadcastFrequency()
        {
            return (Frequency is not default (byte) ? Frequency : 500);
        }
    }
}
