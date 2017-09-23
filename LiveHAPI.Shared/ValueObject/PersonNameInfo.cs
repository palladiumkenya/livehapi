using System;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class PersonNameInfo:IPersonName
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MothersName { get; set; }
        public SourceIdentity SourceIdentity { get; set; }=new SourceIdentity();

        public PersonNameInfo()
        {
        }

        public PersonNameInfo(string firstName, string lastName, SourceIdentity sourceIdentity)
        {
            FirstName = firstName;
            LastName = lastName;
            SourceIdentity = sourceIdentity;
        }
        public PersonNameInfo(string firstName, string middleName, string lastName, SourceIdentity sourceIdentity):this(firstName,lastName,sourceIdentity)
        {
            MiddleName = middleName;
        }
        public PersonNameInfo(string firstName, string middleName, string lastName, string mothersName, SourceIdentity sourceIdentity):this(firstName,middleName,lastName,sourceIdentity)
        {
            MothersName = mothersName;
        }
    }
}