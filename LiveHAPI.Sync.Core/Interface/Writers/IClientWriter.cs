using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Exchange.Messages;

namespace LiveHAPI.Sync.Core.Interface.Writers
{
    public interface IClientWriter<T> :IDisposable where T:ClientMessage
    {
        List<string> Messages { get; }
        List<ErrorResponse> Errors { get; }
        Task<IEnumerable<SynchronizeClientsResponse>> Write(params LoadAction[] actions);
    }
}