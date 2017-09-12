using System;
using System.Collections.Generic;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class ClientInfo:IClient
    {
        public Guid Id { get; set; }
        public string MaritalStatus { get; set; }
        public string KeyPop { get; set; }
        public string OtherKeyPop { get; set; }
        public Guid? PracticeId { get; set; }
        public string PracticeCode { get; set; }
        public PersonInfo Person { get; set; }
        public List<IdentifierInfo> Identifiers { get; set; }=new List<IdentifierInfo>();
        public List<RelationshipInfo> Relationship { get; set; } = new List<RelationshipInfo>();

        


    }
}