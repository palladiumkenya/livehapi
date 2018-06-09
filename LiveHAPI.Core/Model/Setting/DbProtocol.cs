namespace LiveHAPI.Core.Model.Setting
{
    public class DbProtocol
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public DbProtocol()
        {
        }

        public DbProtocol(string server, string database, string user, string password)
        {
            Server = server;
            Database = database;
            User = user;
            Password = password;
        }
    }
}