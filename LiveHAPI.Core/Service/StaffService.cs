using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.ValueModel;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Core.Service
{
    public class StaffService: IStaffService
    {
        private readonly IPersonNameRepository _personNameRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IUserRepository _userRepository;

        public StaffService(IPersonNameRepository personNameRepository, IPersonRepository personRepository, IUserRepository userRepository)
        {
            _personNameRepository = personNameRepository;
            _personRepository = personRepository;
            _userRepository = userRepository;
        }

        public Person Find(PersonIdentity personIdentity)
        {
            var personName = _personNameRepository
                .GetAll(x => x.Source.IsSameAs(personIdentity.Source) &&
                             x.SourceRef.IsSameAs(personIdentity.SourceRef) &&
                             x.SourceSys.IsSameAs(personIdentity.SourceSys))
                .FirstOrDefault();

            if (null != personName)
                return _personRepository.Get(personName.PersonId);

            return null;
        }

        public User EnlistUser(PersonIdentity personIdentity, PersonNameIdentity personNameIdentity, UserIdentity userIdentity)
        {
            var person = Find(personIdentity);

            if (null == person)
            {
                person = Person.CreateUser(personIdentity, personNameIdentity, userIdentity);
                var user = person.Users.First();
                _personRepository.InsertOrUpdate(person);
                _userRepository.InsertOrUpdate(user);
                return user;
            }
            else
            {
                var user = User.Create(userIdentity, personIdentity);
                person.AssignUser(user);
                
                _userRepository.InsertOrUpdate(user);
                return user;
            }
        }
    }
}