using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Exchange
{
    public class InvalidMessage:Entity<Guid>
    {
        public Guid ClientId  { get; set; }
        public MessageType Type { get; set; }
        public Guid? PracticeId { get; set; }
        [MaxLength(4000)]
        public string Message { get; set; }

        public InvalidMessage()
        {
        }

        public InvalidMessage(Guid clientId, MessageType type, string message)
        {
            Id = LiveGuid.NewGuid();
            ClientId = clientId;
            Type = type;
            Message = message;
        }

        public InvalidMessage(Guid clientId, MessageType type, string message, Guid? practiceId):this(clientId,type,message)
        {
            PracticeId = practiceId;
        }

        public override string ToString()
        {
            return $"{Id} |{PracticeId} |{Message}";
        }
    }
}