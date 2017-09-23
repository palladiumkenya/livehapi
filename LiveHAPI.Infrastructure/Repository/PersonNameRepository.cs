using System;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Infrastructure.Repository
{
    public class PersonNameRepository : BaseRepository<PersonName, Guid>, IPersonNameRepository
    {
        public PersonNameRepository(LiveHAPIContext context) : base(context)
        {
        }
    }
}