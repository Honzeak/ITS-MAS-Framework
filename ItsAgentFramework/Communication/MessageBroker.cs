using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItsAgentFramework.Communication;

namespace ItsAgentFramework
{
    internal static class MessageBroker
    {
        private static List<CommunicationSkill<ACLMessage>> _communicationSkillsACL = new();
        private static List<CommunicationSkill<ITSMessage>> _communicationSkillsITS = new();
        public static void Register(CommunicationSkill<ACLMessage> communicationSkill)
        {
            _communicationSkillsACL.Add(communicationSkill);
            //bacha na overriden equals
        }

        public static void Register(CommunicationSkill<ITSMessage> communicationSkill)
        {
            _communicationSkillsITS.Add(communicationSkill);
            //bacha na overriden equals
        }

        public static bool SendMessage(string recipientAgentID, (ACLMessage message, string author) envelope)
        {
            CommunicationSkill<ACLMessage>? recipientQuery = _communicationSkillsACL.FirstOrDefault(skill => skill.AgentID == recipientAgentID);
            return SendMessageInternal(recipientQuery, envelope);
        }

        public static bool SendMessage(string recipientAgentID, (ITSMessage message, string author) envelope)
        {
            CommunicationSkill<ITSMessage>? recipientQuery = _communicationSkillsITS.FirstOrDefault(skill => skill.AgentID == recipientAgentID);
            return SendMessageInternal(recipientQuery, envelope);
        }

        private static bool SendMessageInternal<TMessage>(CommunicationSkill<TMessage>? recipientSkill, (TMessage message, string author) envelope) 
            where TMessage : class
        {
            if (recipientSkill is null)
                return false;

            recipientSkill.ReceivedMessagesQueue.Enqueue(envelope.message);
            return true;
        }

        public static void BroadcastMessage((ACLMessage message, string author) envelope)
        {
            foreach (var skill in _communicationSkillsACL)
            {
                if (skill.AgentID == envelope.author)
                    continue;
                skill.ReceivedMessagesQueue.Enqueue(envelope.message);
            }
        }
        public static void BroadcastMessage((ITSMessage message, string author) envelope)
        {
            foreach (var skill in _communicationSkillsITS)
            {
                if (skill.AgentID == envelope.author)
                    continue;
                skill.ReceivedMessagesQueue.Enqueue(envelope.message);
            }
        }



    }
}
