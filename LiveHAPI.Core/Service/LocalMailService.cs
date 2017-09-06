using System.Diagnostics;
using LiveHAPI.Core.Interfaces.Services;

namespace LiveHAPI.Core.Service
{
    public class LocalMailService:IMailService
    {
        public void Send(string subject, string message, string to = "", string from = "")
        {
            Debug.WriteLine($"From:{from}");
            Debug.WriteLine($"To:{to}");
            Debug.WriteLine($"SUBJECT:{subject}");
            Debug.WriteLine(new string('-', 50));
            Debug.WriteLine($"MESSAGE:{message}");
            Debug.WriteLine(new string('*', 50));
            
        }
    }
}