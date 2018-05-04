using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;
using LiveHAPI.Sync.Core.Enum;

namespace LiveHAPI.Core.Model.Exchange
{
    public class ClientStage:Entity<Guid>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int MaritalStatus { get; set; }
        public int Sex { get; set; }
        public string Serial { get; set; }
        public string Landmark { get; set; }
        public string Phone { get; set; }
        public int KeyPop { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DateOfBirthPrecision { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }

        public ClientStage()
        {
            SyncStatus = SyncStatus.Staged;
            StatusDate=DateTime.Now;
        }

        public static ClientStage Create(Person person, SubscriberSystem subscriber)
        {
            var clientStage=new ClientStage();

            if (person.Names.Any())
            {
                clientStage.FirstName = person.Names.First().FirstName;
                clientStage.MiddleName = person.Names.First().MiddleName;
                clientStage.LastName = person.Names.First().LastName;               
            }

            clientStage.Sex = subscriber.GetTranslation(person.Gender, "Gender", "0").SafeConvert<int>();

            if (person.Clients.Any())
            {
                var client = person.Clients.First();
                clientStage.MaritalStatus = subscriber.GetTranslation(client.MaritalStatus, "HTSMaritalStatus", "0").SafeConvert<int>();
            }

            return clientStage;
        }
    }
}