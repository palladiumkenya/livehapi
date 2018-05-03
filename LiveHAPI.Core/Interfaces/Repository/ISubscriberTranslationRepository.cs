using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface ISubscriberTranslationRepository : IRepository<SubscriberTranslation,Guid>
    {
        void Sync(IEnumerable<SubscriberTranslation> translations);
    }
}