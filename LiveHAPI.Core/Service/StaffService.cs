using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.ValueObject;

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


        public Person Find(Identity identity)
        {
            var personName = _personNameRepository
                .GetAll(x => x.Source.IsSameAs(identity.Source) &&
                             x.SourceRef.IsSameAs(identity.SourceRef) &&
                             x.SourceSys.IsSameAs(identity.SourceSys))
                .FirstOrDefault();

            if (null != personName)
                return _personRepository.Get(personName.PersonId);

            return null;
        }

        public User EnlistUser(Identity identity, PersonNameInfo personNameInfo, UserInfo userInfo,Guid practiceId)
        {

            var person = Find(identity);

            if (null == person)
            {
                person = Person.CreateUser(identity, personNameInfo, userInfo,practiceId);
                var user = person.Users.First();
                _personRepository.InsertOrUpdate(person);
                _personRepository.Save();
                _userRepository.InsertOrUpdate(user);
                _userRepository.Save();
                return user;
            }
            else
            {
                var user = User.Create(userInfo, identity, practiceId);
                var personName = PersonName.Create(personNameInfo, identity);

                var updatedNames= person.AssignName(personName);
                var updateUser=person.AssignUser(user);

                _personNameRepository.InsertOrUpdate(updatedNames);
                _personNameRepository.Save();
                _userRepository.InsertOrUpdate(updateUser);
                _userRepository.Save();

                return updateUser;
            }
        }

        public void SyncUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}