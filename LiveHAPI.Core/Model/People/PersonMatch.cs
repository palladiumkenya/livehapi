using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.People
{
    public class PersonMatch
    {
        public Person Person { get; set; }
        public decimal Rank { get; set; }
        
        public RemoteClientInfo RemoteClient  { get; set; }

        public PersonMatch()
        {
            Person=new Person();
            RemoteClient = new RemoteClientInfo();
        }

        public PersonMatch(Person person, decimal rank)
        {
            RemoteClient=new RemoteClientInfo();
            Person = person;
            Rank = rank;
            RemoteClient.Client = Person.GetClientInfo();
        }


        public override string ToString()
        {
            return $"{Person} <{Rank}>";
        }
    }
}