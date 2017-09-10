using System.ComponentModel.DataAnnotations;

namespace LiveHAPI.Core.ValueModel
{
    public class PersonNameIdentity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MothersName { get; set; }

        public PersonNameIdentity()
        {
        }

        public PersonNameIdentity(string firstName, string middleName, string lastName, string mothersName="")
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            MothersName = mothersName;
        }
    }
}