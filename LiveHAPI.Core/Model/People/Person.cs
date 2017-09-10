using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Core.ValueModel;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class Person:Entity<Guid>, IPerson
    {
        [MaxLength(10)]
        public  string Gender { get; set; }
        public  DateTime? BirthDate { get; set; }
        public  bool? BirthDateEstimated { get; set; }

        public ICollection<PersonName> Names { get; set; } = new List<PersonName>();
        public  ICollection<PersonAddress> Addresses { get; set; }=new List<PersonAddress>();        
        public  ICollection<PersonContact> Contacts { get; set; }=new List<PersonContact>();
        public  ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Provider> Providers { get; set; }=new List<Provider>();        
        public ICollection<Client> Clients { get; set; } = new List<Client>();

        public Person()
        {
            Id = LiveGuid.NewGuid();
        }

        public PersonName AssignName(PersonName name)
        {
            name.PersonId = Id;

            var personName = Names
                .FirstOrDefault(x => x.Source.IsSameAs(name.Source) &&
                                     x.SourceRef.IsSameAs(name.SourceRef) &&
                                     x.SourceSys.IsSameAs(name.SourceSys));
            if (null != personName)
            {
                Names.Remove(personName);
                personName.ChangeTo(name);
                Names.Add(personName);
                return personName;
            }

            Names.Add(name);
            return name;
        }

        public void AssignAddress(PersonAddress address)
        {
            address.PersonId = Id;

            if (Addresses.Any(x => x.Source.ToLower() == address.Source.ToLower() &&
                               x.SourceRef.ToLower() == address.SourceRef.ToLower()))
            {
                var personAddress = Addresses.First(x => x.Source.ToLower() == address.Source.ToLower() &&
                                                  x.SourceRef.ToLower() == address.SourceRef.ToLower());

                Addresses.Remove(personAddress);
                personAddress.ChangeTo(address);
                Addresses.Add(personAddress);
            }
            else
            {
                Addresses.Add(address);
            }
        }

        public void AssignContact(PersonContact contact)
        {
            contact.PersonId = Id;

            if (Contacts.Any(x => x.Source.ToLower() == contact.Source.ToLower() &&
                               x.SourceRef.ToLower() == contact.SourceRef.ToLower()))
            {
                var personContact = Contacts.First(x => x.Source.ToLower() == contact.Source.ToLower() &&
                                                  x.SourceRef.ToLower() == contact.SourceRef.ToLower());

                Contacts.Remove(personContact);
                personContact.ChangeTo(contact);
                Contacts.Add(personContact);
            }
            else
            {
                Contacts.Add(contact);
            }
        }

        public User AssignUser(User user)
        {
            if (null == user)
                throw new ArgumentException("No user!");

            user.PersonId = Id;

            var personUser = Users
                .FirstOrDefault(
                    x => x.Source.IsSameAs(user.Source) &&
                         x.SourceRef.IsSameAs(user.SourceRef) &&
                         x.SourceSys.IsSameAs(user.SourceSys));
            if (null != personUser)
            {
                Users.Remove(personUser);
                personUser.ChangeTo(user);
                Users.Add(personUser);
                return personUser;
            }

            Users.Add(user);
            return user;
        }

        public void AssignProvider(Provider provider)
        {
            if (null == provider)
                throw new ArgumentException("No provider!");

            provider.PersonId = Id;

            var personProvider = Providers.FirstOrDefault(
                x => x.Source.IsSameAs(provider.Source) &&
                     x.SourceRef.IsSameAs(provider.SourceRef) &&
                     x.SourceSys.IsSameAs(provider.SourceSys));

            if (null!=personProvider)
            {
                Providers.Remove(personProvider);
                personProvider.ChangeTo(provider);
                Providers.Add(personProvider);
            }
            else
            {
                Providers.Add(provider);
            }
        }


        public static Person CreateUser(PersonIdentity personIdentity, PersonNameIdentity personNameIdentity, UserIdentity userIdentity)
        {
            var person=new Person();
            
            var user = User.Create(userIdentity,personIdentity);
            var personName = PersonName.Create(personNameIdentity, personIdentity);
            person.AddName(personName);
            person.AssignUser(user);

            return person;
        }

        private void AddName(PersonName personName)
        {
            personName.PersonId = Id;
            Names.Add(personName);
        }
    }
}