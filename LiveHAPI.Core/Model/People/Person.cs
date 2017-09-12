using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

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
        private Person(Guid id,string gender, DateTime? birthDate, bool? birthDateEstimated): base(id)
        {
            Gender = gender;
            BirthDate = birthDate;
            BirthDateEstimated = birthDateEstimated;
        }
        private Person(string gender, DateTime? birthDate, bool? birthDateEstimated) : this(LiveGuid.NewGuid(),gender,birthDate,birthDateEstimated)
        {
        }
        public static Person CreateClient(PersonInfo personInfo)
        {
            var person = new Person(personInfo.Id, personInfo.Gender, personInfo.BirthDate, personInfo.BirthDateEstimated);

            var personNames = PersonName.Create(personInfo);
            person.AddNames(personNames);

            var addresses = PersonAddress.Create(personInfo);
            person.AddAddresss(addresses);

            var contacts = PersonContact.Create(personInfo);
            person.AddContacts(contacts);

            return person;
        }
        public void UpdateClient(PersonInfo personInfo)
        {
            Gender = personInfo.Gender;
            BirthDate = personInfo.BirthDate;
            BirthDateEstimated = personInfo.BirthDateEstimated;

            var personNames = PersonName.Create(personInfo);
            Names.Clear();
            AddNames(personNames);

            var addresses = PersonAddress.Create(personInfo);
            Addresses.Clear();
            AddAddresss(addresses);

            var contacts = PersonContact.Create(personInfo);
            Contacts.Clear();
            AddContacts(contacts);
        }

        public static Person CreateUser(UserInfo userInfo)
        {
            var person = new Person();
            var personName = PersonName.Create(userInfo.PersonNameInfo);
            person.AddName(personName);
            return person;
        }
        public static Person CreateProvider(ProviderInfo providerInfo)
        {
            var person = new Person();
            var personName = PersonName.Create(providerInfo.PersonNameInfo);
            person.AddName(personName);
            return person;
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
        //Provider
        public Provider AssignProvider(Provider provider)
        {
            if (null == provider)
                throw new ArgumentException("No Provider!");

            provider.PersonId = Id;

            var personProvider = Providers
                .FirstOrDefault(
                    x => x.Source.IsSameAs(provider.Source) &&
                         x.SourceRef.IsSameAs(provider.SourceRef) &&
                         x.SourceSys.IsSameAs(provider.SourceSys));

            if (null != personProvider)
            {
                Providers.Remove(personProvider);
                personProvider.ChangeTo(provider);
                Providers.Add(personProvider);
                return personProvider;
            }

            Providers.Add(provider);
            return provider;
        }


        private void AddNames(List<PersonName> names)
        {
            foreach (var personName in names)
            {
                AddName(personName);
            }
        }
        private void AddName(PersonName name)
        {
            name.PersonId = Id;
            Names.Add(name);
        }

        private void AddAddresss(List<PersonAddress> personNames)
        {
            foreach (var personName in personNames)
            {
                AddAddress(personName);
            }
        }
        private void AddAddress(PersonAddress address)
        {
            address.PersonId = Id;
            Addresses.Add(address);
        }

        private void AddContacts(List<PersonContact> contacts)
        {
            foreach (var personName in contacts)
            {
                AddContact(personName);
            }
        }
        private void AddContact(PersonContact contact)
        {
            contact.PersonId = Id;
            Contacts.Add(contact);
        }

        public override string ToString()
        {
            var info = $" {Gender}|{BirthDate:yyyy MMMM dd}";
            var names = Names.Count > 0 ? Names.First().FullName : "";
            return $"{names}{info}";
        }
    }
}