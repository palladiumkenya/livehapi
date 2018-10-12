using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Humanizer;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Exchange
{
    public class ClientStage:Entity<Guid>
    {
        public string Serial { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// "ESTIMATED/EXACT"
        /// </summary>
        public string DateOfBirthPrecision { get; set; }
        public int Sex { get; set; }
        public int KeyPop { get; set; }

        public int? County { get; set; }
        public int? SubCounty { get; set; }
        public int? Ward { get; set; }

        public string Landmark { get; set; }
        public string Phone { get; set; }
        public int MaritalStatus { get; set; }

        public int? Education { get; set; }
        public int? Completion { get; set; }
        public int? Occupation { get; set; }

        public DateTime RegistrationDate { get; set; }

        public Guid ClientId { get; set; }
       
        public bool IsIndex { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }
        public int UserId { get; set; }
        public Guid PracticeId { get; set; }
        public string SiteCode { get; set; }

        [NotMapped]
        public string Names
        {
            get { return $"{FirstName} {MiddleName} {LastName}"; }
        }
       
        [NotMapped]
        public string TimeAgo => StatusDate.Humanize(false);
        

        public ClientStage()
        {
            SyncStatus = SyncStatus.Staged;
            StatusDate=DateTime.Now;
        }

        public static ClientStage Create(Person person, SubscriberSystem subscriber)
        {
            var clientStage=new ClientStage();

            if (null != person.PersonClient)
            {
                clientStage.Id = person.PersonClient.Id;
                clientStage.ClientId = person.PersonClient.Id;
            }
            else
            {
                clientStage.Id = LiveGuid.NewGuid();
            }


            if (null != person.PersonName)
            {
                clientStage.FirstName = person.PersonName.FirstName;
                clientStage.MiddleName = person.PersonName.MiddleName;
                clientStage.LastName = person.PersonName.LastName;
                clientStage.NickName = person.PersonName.NickName;
                if (person.Addresses.Any())
                {
                    clientStage.Landmark = person.Addresses.First().Landmark;
                    clientStage.County= person.Addresses.First().CountyId;
                    clientStage.SubCounty= person.Addresses.First().SubCountyId;
                    clientStage.Ward= person.Addresses.First().WardId;
                }
                if (person.Contacts.Any())
                {
                    clientStage.Phone = person.Contacts.First().Phone.HasValue
                        ? person.Contacts.First().Phone.Value.ToString()
                        : string.Empty;
                }
            }
            
            clientStage.DateOfBirth = person.HasDOB() ? person.BirthDate.Value : new DateTime(1900, 1, 1);
            clientStage.DateOfBirthPrecision = person.HasDOBEstimate()
                ? (person.BirthDateEstimated.Value ? "EXACT" : "ESTIMATED")
                : "ESTIMATED";
            clientStage.Sex = subscriber.GetTranslation(person.Gender, "Gender", "0").SafeConvert<int>();

            var clientt = person.PersonClient;
            if (null != clientt)
            {
                clientStage.PracticeId = clientt.PracticeId;
                clientStage.KeyPop =
                    subscriber.GetTranslation(clientt.KeyPop, "HTSKeyPopulation", "0").SafeConvert<int>();
                clientStage.MaritalStatus = subscriber.GetTranslation(clientt.MaritalStatus, "HTSMaritalStatus", "0")
                    .SafeConvert<int>();
                
                clientStage.UserId = subscriber.GetEmrUserId(clientt.UserId);
            }

            //Education
            clientStage.Education = subscriber.GetTranslation(clientt.Education, "EducationalLevel", "0")
                .SafeConvert<int>();
            clientStage.Completion = subscriber.GetTranslation(clientt.Completion, "EducationOutcome", "0")
                .SafeConvert<int>();

            //Occupation
            clientStage.Occupation = subscriber.GetTranslation(clientt.Occupation, "HTSOccupation", "0")
                .SafeConvert<int>();


            clientStage.IsIndex = person.IsHtsClient;
            if (clientStage.IsIndex)
            {
                var client = person.PersonClient;

                if (null != client.HtsEnrollment)
                {
                    clientStage.Serial = client.HtsEnrollment.Identifier;
                    clientStage.RegistrationDate = client.HtsEnrollment.RegistrationDate;
                }         
            }
            else
            {
                clientStage.RegistrationDate = person.ContactRegDate;
            }
           
            return clientStage;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} [{(IsIndex?"Index":"Secondary")}], [{ClientId}]";
        }
    }
}