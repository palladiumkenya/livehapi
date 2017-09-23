using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ObsLinkageRepository : BaseRepository<ObsLinkage, Guid> ,IObsLinkageRepository
    {
        public ObsLinkageRepository(LiveHAPIContext context) : base(context)
        {
        }


        public void ReplaceAll(Guid encounterId, IEnumerable<ObsLinkage> obses)
        {
            var existingObsLinkagees = Context.ObsLinkages.Where(x => x.EncounterId == encounterId).ToList();
            if (existingObsLinkagees.Count > 0)
            {
                Context.RemoveRange(existingObsLinkagees);
                Context.SaveChanges();
            }
            Insert(obses);
        }
    }
}
