using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IPersonRepository : IRepository<Person,Guid>
    {
        Model.People.Person GetDemographics(Guid id);
        IEnumerable<Person> GetStaff();
    }
}