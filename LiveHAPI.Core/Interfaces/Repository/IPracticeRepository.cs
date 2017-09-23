using System;
using LiveHAPI.Core.Model.Network;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IPracticeRepository : IRepository<Practice,Guid>
    {
        Practice GetByCode(string code);
    }
}