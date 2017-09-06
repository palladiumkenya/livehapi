namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IMailService
    {
        void Send(string subject, string message, string to = "", string from = "");
    }
}