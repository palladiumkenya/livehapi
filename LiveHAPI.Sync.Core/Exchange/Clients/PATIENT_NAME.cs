namespace LiveHAPI.Sync.Core.Exchange.Clients
{
    public class PATIENT_NAME
    {
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string NICK_NAME { get; set; }

        public PATIENT_NAME()
        {
        }

        private PATIENT_NAME(string firstName, string middleName, string lastName,string nickName)
        {
            FIRST_NAME = firstName;
            MIDDLE_NAME = middleName;
            LAST_NAME = lastName;
            NICK_NAME = nickName;
        }

        public static PATIENT_NAME Create(string firstName, string middleName, string lastName, string nickName)
        {
            return new PATIENT_NAME(firstName, middleName, lastName, nickName);
        }
    }
}