using System;
using System.Collections.Generic;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class ClientNetwork:Entity<Guid>
    {
        public Contact Primary { get; set; }=new Contact();
        public Contact Secondary { get; set; }=new Contact();
        public DateTime Generated { get; set; }

        public ClientNetwork()
        {
            Id = LiveGuid.NewGuid();
            Generated=DateTime.Now;
        }

        public ClientNetwork(Contact primary, Contact secondary):this()
        {
            Primary = primary;
            Secondary = secondary;
        }

        public override string ToString()
        {
            return $"{Id} {Primary} {Secondary} ({Generated:yyyy MMMM dd})";
        }
    }
 }