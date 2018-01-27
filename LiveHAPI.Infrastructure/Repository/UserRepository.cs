using System;
using System.Diagnostics;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        public UserRepository(LiveHAPIContext context) : base(context)
        {
        }

        public User GetByUsername(string username)
        {
            return Context.Users.FirstOrDefault(x => x.UserName.ToLower().Trim() == username.ToLower().Trim());
        }

        public void Sync(User user)
        {
            var existingUser = GetByUsername(user.UserName);
            if (null != existingUser)
            {
                existingUser.UpdateTo(user);
                var personName= Context.PersonNames.FirstOrDefault(x => x.PersonId == existingUser.PersonId);
                if (null != personName)
                {
                    personName.UpdateTo(existingUser.Source, existingUser.SourceSys);
                    Context.PersonNames.Update(personName);
                }

                Update(existingUser);
            }
            else
            {
                //set PracticeId
                var practiceId = GetPracticeId();

                //Person
                var person = new Person();
                var personName = new PersonName();
                personName.FirstName = user.Source;
                personName.LastName = user.SourceSys;
                person.AddName(personName);
                
                var provider = new Provider();
                provider.ProviderTypeId = "HW";
                provider.PracticeId = practiceId;
                person.AddProvider(provider);

                Context.Persons.Add(person);
                Context.SaveChanges();

                user.PersonId = person.Id;
                user.PracticeId = practiceId;

                //Provider
                Insert(user);
            }
        }

        private Guid? GetPracticeId()
        {
            var prac = Context.Practices.FirstOrDefault(x => x.IsDefault && x.PracticeTypeId == "Facility");
            return prac?.Id;
        }
    }
}