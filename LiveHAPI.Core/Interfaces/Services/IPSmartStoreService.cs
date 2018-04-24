using System.Collections.Generic;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IPSmartStoreService
    {
        void Sync(List<PSmartStoreInfo> encounterInfos);
        void Sync(PSmartStoreInfo encounterInfo);
    }
}