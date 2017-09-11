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
        private readonly IActivationService _activationService;
        private readonly IPersonNameRepository _personNameRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProviderRepository _providerRepository;

        public StaffService(IPersonNameRepository personNameRepository, IPersonRepository personRepository, IUserRepository userRepository,IProviderRepository providerRepository, IActivationService activationService)
        {
            _personNameRepository = personNameRepository;
            _personRepository = personRepository;
            _userRepository = userRepository;
            _activationService = activationService;
            _providerRepository = providerRepository;
        }

        public Person Find(PersonInfo personInfo)
        {
            var identity = personInfo.Identity;

            var personName = _personNameRepository
                .GetAll(x => x.Source.IsSameAs(identity.Source) &&
                             x.SourceRef.IsSameAs(identity.SourceRef) &&
                             x.SourceSys.IsSameAs(identity.SourceSys))
                .FirstOrDefault();

            if (null != personName)
                return _personRepository.Get(personName.PersonId);

            return null;
        }
        public User EnlistUser(UserInfo userInfo, Guid practiceId)
        {
            var person = Find(userInfo.PersonInfo);
            var user = User.Create(userInfo, practiceId);

            if (null == person)
            {
                person = Person.CreateUser(userInfo);

                var newUser = person.AssignUser(user);

                _personRepository.InsertOrUpdate(person);
                _personRepository.Save();
                _userRepository.InsertOrUpdate(newUser);
                _userRepository.Save();
                return newUser;
            }

            var personName = PersonName.Create(userInfo.PersonInfo);

            var updatedNames = person.AssignName(personName);
            var updateUser = person.AssignUser(user);

            _personNameRepository.InsertOrUpdate(updatedNames);
            _personNameRepository.Save();
            _userRepository.InsertOrUpdate(updateUser);
            _userRepository.Save();

            return updateUser;

        }
        public IEnumerable<User> EnlistUsers(string practiceCode, IEnumerable<UserInfo> userInfos)
        {
            var practice = _activationService.EnrollPractice(practiceCode);

            if (null == practice)
                throw new ArgumentException("Facility is not Registered!");

            var users = new List<User>();

            foreach (var userInfo in userInfos)
            {
                var enlisted = EnlistUser(userInfo, practice.Id);
                if(null!=enlisted)
                    users.Add(enlisted);
            }

            return users;
        }

        public Provider EnlistProvider(ProviderInfo providerInfo, Guid practiceId)
        {
            var person = Find(providerInfo.PersonInfo);
            var provider = Provider.Create(providerInfo, practiceId);

            if (null == person)
            {
                person = Person.CreateProvider(providerInfo);

                var newProvider = person.AssignProvider(provider);

                _personRepository.InsertOrUpdate(person);
                _personRepository.Save();
                _providerRepository.InsertOrUpdate(newProvider);
                _providerRepository.Save();
                return newProvider;
            }

            var personName = PersonName.Create(providerInfo.PersonInfo);

            var updatedNames = person.AssignName(personName);
            var updateProvider = person.AssignProvider(provider);

            _personNameRepository.InsertOrUpdate(updatedNames);
            _personNameRepository.Save();
            _providerRepository.InsertOrUpdate(updateProvider);
            _providerRepository.Save();

            return updateProvider;
        }
        public IEnumerable<Provider> EnlistProviders(string practiceCode, IEnumerable<ProviderInfo> providerInfos)
        {
            var practice = _activationService.EnrollPractice(practiceCode);

            if (null == practice)
                throw new ArgumentException("Facility is not Registered!");

            var providers= new List<Provider>();

            foreach (var providerInfo in providerInfos)
            {
                var enlisted = EnlistProvider(providerInfo, practice.Id);
                if (null != enlisted)
                    providers.Add(enlisted);
            }

            return providers;
        }

        public void SyncUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}