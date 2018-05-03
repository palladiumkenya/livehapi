using System;
using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib.Extensions;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.Subscriber;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;

namespace LiveHAPI.Infrastructure.Repository
{
    public class SubscriberTranslationRepository : BaseRepository<SubscriberTranslation, Guid>,ISubscriberTranslationRepository
    {
        private SubscriberSystem _defaultSystem;

        public SubscriberTranslationRepository(LiveHAPIContext context) : base(context)
        {
        }

        public SubscriberTranslation GetByLookupItem(string masterName,string itemName)
        {
            return GetDbConnection().GetAll<SubscriberTranslation>()
                .FirstOrDefault(x => x.SubRef.ToLower() == masterName.ToLower() &&
                                     x.SubDisplay.ToLower() == itemName.ToLower() &&
                                     x.SubscriberSystemId==_defaultSystem.Id);
        }

        public void Sync(IEnumerable<SubscriberTranslation> translations)
        {
            _defaultSystem = GetDbConnection().GetAll<SubscriberSystem>().FirstOrDefault(x => x.IsDefault);

            var updateList = new List<SubscriberTranslation>();

            foreach (var practice in updateList)
            {
                var exisitngPractice = GetByLookupItem(practice.SubRef, practice.SubDisplay);
                if (null != exisitngPractice)
                {
                    exisitngPractice.UpdateTo(practice);
                    updateList.Add(exisitngPractice);
                }
            }
            GetDbConnection().BulkUpdate(updateList);
        }
    }
}