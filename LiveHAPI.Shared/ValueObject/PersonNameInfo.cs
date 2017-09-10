namespace LiveHAPI.Shared.ValueObject
{
    public class PersonNameInfo
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MothersName { get; set; }
        Identity Identity { get; set; }

        public PersonNameInfo()
        {
        }

        public PersonNameInfo(string firstName, string lastName, Identity identity)
        {
            FirstName = firstName;
            LastName = lastName;
            Identity = identity;
        }
        public PersonNameInfo(string firstName, string middleName, string lastName, Identity identity):this(firstName,lastName,identity)
        {
            MiddleName = middleName;
        }
        public PersonNameInfo(string firstName, string middleName, string lastName, string mothersName, Identity identity):this(firstName,middleName,lastName,identity)
        {
            MothersName = mothersName;
        }
    }
}