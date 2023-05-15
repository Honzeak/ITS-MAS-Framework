using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsAgentFramework.Communication
{
    public class ACLMessage
    {
        public CA CommunicativeAct;

        public string? Sender { get; private set; }
        public string? Receiver { get; private set; }
        public string? Content { get; private set; }
        public string? ReplyWith { get; private set; }
        public string? InReplyTo { get; private set; }
        public string? Envelope { get; private set; }
        public string? Language { get; private set; }
        public string? Ontology { get; private set; }
        public string? ReplyBy { get; private set; }
        public string? Protocol { get; private set; }
        public string? ConversationID { get; private set; }

        public ACLMessage(CA communicativeAct, string? sender, string? receiver, string? content, string? replyWith, string? inReplyTo,
            string? envelope, string? language, string? ontology, string? replyBy, string? protocol, string? conversationID)
        {
            CommunicativeAct = communicativeAct;
            Sender = sender;
            Receiver = receiver;
            Content = content;
            ReplyWith = replyWith;
            InReplyTo = inReplyTo;
            Envelope = envelope;
            Language = language;
            Ontology = ontology;
            ReplyBy = replyBy;
            Protocol = protocol;
            ConversationID = conversationID;
        }

        public enum CA
        {
            acceptProposal,
            agree,
            cancel,
            cfp,
            confirm,
            disconfirm,
            failure,
            inform,
            informIf,
            informRef,
            notUnderstood,
            propose,
            queryIf,
            querRef,
            refuse,
            refectProposal,
            request,
            requestWhen,
            requestWhenever,
            subscribe
        }

    }
}
