using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItsAgentFramework.Components;

namespace ItsAgentFramework.Communication
{
    public abstract class CommunicationSkill<TMessage> : Skill where TMessage : class?
    {

        public string AgentID { get; private set; }
        public Queue<TMessage> ReceivedMessagesQueue = new();
        public event EventHandler<CommunicationEventArgs<TMessage>>? MessageReceived;
        public ICollection<TMessage> BroadcastedMessages = new Collection<TMessage>();

        public CommunicationSkill(string agentID, ICollection<TMessage>? broadcastMessages = null)
        {
            AgentID = agentID;
            if (broadcastMessages is not null)
                BroadcastedMessages = broadcastMessages;
        }

        protected override void Act()
        {
            if (BroadcastedMessages is not null)
            {
                foreach (var message in BroadcastedMessages)
                {
                    Broadcast((message, AgentID));
                }
            }
            while (ReceivedMessagesQueue.TryDequeue(out var message))
            {
                 OnMessageReceived(message);
            }
        }

        protected abstract void Broadcast((TMessage message, string author) envelope);

        protected virtual void OnMessageReceived(TMessage message)
        {
            MessageReceived?.Invoke(this, new CommunicationEventArgs<TMessage>(message));
        }
    }

    public class CommunicationEventArgs<TMessage> : EventArgs
    {
        public TMessage Message;
        public CommunicationEventArgs(TMessage message) => Message = message;

    }
}
