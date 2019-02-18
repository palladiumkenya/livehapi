using System;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Sync.Core.Exchange;

namespace LiveHAPI.Sync.Core.Model
{
    public class SyncReport
    {
        public SyncStatus Status { get; private set; }
        public DateTime ReportDate { get; private set; }
        public Exception Exception { get; private set; }
        public string ExceptionInfo { get; private set; }
        public Guid ClientId { get; private set; }
        public string Endpoint { get; private set; }
        public string Message { get; private set; }
        public SynchronizeClientsResponse Response { get; private set; }

        public bool IsSuccess => null == Exception;
        public bool HasResponse => null != Response;

        private SyncReport()
        {
            ReportDate = DateTime.Now;
        }

        private SyncReport(Guid clientId, string endpoint, SyncStatus status,
            SynchronizeClientsResponse response) : this()
        {
            Response = response;
            Status = status;
            ClientId = clientId;
            Endpoint = endpoint;
        }

        private SyncReport(Guid clientId, string endpoint, SyncStatus status, SynchronizeClientsResponse response,
            string message, Exception exception, string exceptionInfo) : this(clientId, endpoint, status, response)
        {
            Message = message;
            Exception = exception;
            ExceptionInfo = exceptionInfo;
        }

        public static SyncReport GenerateSuccess(Guid clientId, string endpoint, SynchronizeClientsResponse response)
        {
            return new SyncReport(clientId, endpoint, SyncStatus.SentSuccess, response);
        }

        public static SyncReport GenerateFail(Guid clientId, string endpoint, SynchronizeClientsResponse response,
            string message, Exception exception, string exceptionInfo)
        {
            return new SyncReport(clientId, endpoint, SyncStatus.SentFail, response, message, exception, exceptionInfo);
        }
    }
}