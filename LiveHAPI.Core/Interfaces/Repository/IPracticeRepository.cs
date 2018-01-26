using System;
using LiveHAPI.Core.Dispatcher;
using LiveHAPI.Core.Model.Network;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IPracticeRepository : IRepository<Practice,Guid>
    {
        Practice GetDefault();
        Practice GetByCode(string code);
        void Sync(Practice practice);
        void ResetDefault(Guid practiceId);
    }
}