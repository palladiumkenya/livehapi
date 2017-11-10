using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IClientRepository : IRepository<Client,Guid>
    {
        Client GetClient(Guid id);
        IEnumerable<PersonMatch> GetById(Guid id);
        IEnumerable<PersonMatch> Search(string searchItem);
    }
}