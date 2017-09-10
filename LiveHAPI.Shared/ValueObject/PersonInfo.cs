namespace LiveHAPI.Shared.ValueObject
{
    public class PersonInfo
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MothersName { get; set; }
        public Identity Identity { get; set; }=new Identity();

        public PersonInfo()
        {
        }

        public PersonInfo(string firstName, string lastName, Identity identity)
        {
            FirstName = firstName;
            LastName = lastName;
            Identity = identity;
        }
        public PersonInfo(string firstName, string middleName, string lastName, Identity identity):this(firstName,lastName,identity)
        {
            MiddleName = middleName;
        }
        public PersonInfo(string firstName, string middleName, string lastName, string mothersName, Identity identity):this(firstName,middleName,lastName,identity)
        {
            MothersName = mothersName;
        }
    }
}