using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAgentFramework.Communication
{
    public class ITSCommunicationSkill : CommunicationSkill<ITSMessage>
    {
        private TimeSpan lastBroadcasted = DateTime.Today.TimeOfDay;
        public event EventHandler<CommunicationEventArgs<CAMessage>>? CAMReceived;
        public event EventHandler<CommunicationEventArgs<DENMessage>>? DENMReceived;
        
        public ITSCommunicationSkill(string agentID, bool subscribeBroadcast, 
            ICollection<ITSMessage>? broadcastedMessages = null) 
            : base(agentID, broadcastedMessages) {
            if (subscribeBroadcast)
            {
                MessageBroker.Register(this);
            }
        }

        protected override void OnMessageReceived(ITSMessage message)
        {
            base.OnMessageReceived(message);
            if (message is CAMessage)
                CAMReceived?.Invoke(this, new CommunicationEventArgs<CAMessage>((CAMessage)message));
            if (message is DENMessage)
                DENMReceived?.Invoke(this, new CommunicationEventArgs<DENMessage>((DENMessage)message));
        }



        protected override void Broadcast((ITSMessage message, string author) envelope)
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            if ((now - lastBroadcasted).TotalMilliseconds < envelope.message.GetBroadcastFrequency())
                return;

            lastBroadcasted = now;
            MessageBroker.BroadcastMessage(envelope);
            Console.WriteLine($"Agent {AgentID}: Broadcasting message {envelope.message.GetType()}, ID {envelope.message.Header.MessageID}");
        }
    }

    public class ACLCommunicationSkill : CommunicationSkill<ACLMessage>
    {
        public ACLCommunicationSkill(string agentID, ICollection<ACLMessage>? broadcastMessage = null) 
            : base(agentID, broadcastMessage) { }

        protected override void Broadcast((ACLMessage message, string author) envelope)
        {
            MessageBroker.BroadcastMessage(envelope);
        }
    }
}
