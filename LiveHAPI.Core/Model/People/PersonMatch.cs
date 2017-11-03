using System.Collections.Generic;

namespace LiveHAPI.Core.Model.People
{
    public class PersonMatch
    {
        public Person Person { get; set; }
        public decimal Rank { get; set; }

        public PersonMatch(Person person, decimal rank)
        {
            Person = person;
            Rank = rank;
        }
        public override string ToString()
        {
            return $"{Person} <{Rank}>";
        }
    }
}