using System;
using System.Collections.Generic;
using LiveHAPI.Core.Dispatcher;
using LiveHAPI.Core.Model.Network;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IPracticeRepository : IRepository<Practice,Guid>
    {
        Practice GetDefault();
        IEnumerable<Practice> GetAllDefault();
        Practice GetByCode(string code);
        Practice GetByFacilityCode(string code);
        void Sync(Practice practice);
        void ResetDefault(Guid practiceId);

        void Sync(IEnumerable<Practice> practices);
        void ResetDefault(List<Guid> practiceIds);

    }
}