using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IPersonRepository : IRepository<Person,Guid>
    {
        Model.People.Person GetProvider(Guid id);
        Model.People.Person GetDemographics(Guid id);
        IEnumerable<Person> GetStaff();
        IEnumerable<PersonMatch> Search(string searchItem);
        IEnumerable<PersonMatch> SearchSite(string site, string searchItem);
        IEnumerable<PersonMatch> GetByCohort(SubscriberCohort cohort);
        IEnumerable<PersonMatch> GetBySiteCohort(string site,SubscriberCohort cohort);
        IEnumerable<Person> GetAllClients();
        IEnumerable<Person> GetAllIndexClients();
        IEnumerable<Person> GetAllSecondaryClients();
        IEnumerable<Person> GetContacts(Guid clientId);
    }
}