using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.People
{
    public class PersonMatch
    {
        public Person Person { get; set; }
        public decimal Rank { get; set; }
        
        public RemoteClientInfo RemoteClient  { get; set; }
        public Guid PracticeId => GetId();

      

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

        public bool IsInPractice(Guid practiceId)
        {
            if (!practiceId.IsNullOrEmpty() &&
                null != RemoteClient &&
                RemoteClient.HasPractice)
            {
                return practiceId == RemoteClient.Client.PracticeId.Value;
            }

            return false;
        }


        private Guid GetId()
        {
            if (null != RemoteClient && RemoteClient.HasPractice)
            {
                return RemoteClient.Client.PracticeId.Value;
            }

            return Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Person} <{Rank}>";
        }
    }
}