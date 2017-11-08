﻿using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Subscriber;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Infrastructure.Repository
{
    public class SubscriberSystemRepository : BaseRepository<SubscriberSystem, Guid>, ISubscriberSystemRepository
    {
        public SubscriberSystemRepository(LiveHAPIContext context) : base(context)
        {
        }

        public SubscriberSystem GetDefault()
        {
            return Context.SubscriberSystems
                .Include(x => x.Configs)
                .Include(x => x.Actions)
                .Include(x => x.Messages)
                .Include(x => x.Maps)
                .Include(x=>x.Translations)
                .Include(x => x.Cohorts)
                .FirstOrDefault(x=>x.IsDefault);
        }
    }
}