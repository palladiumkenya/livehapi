using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ObsPartnerTraceResultRepository : BaseRepository<ObsPartnerTraceResult, Guid> , IObsPartnerTraceResultRepository
    {
        public ObsPartnerTraceResultRepository(LiveHAPIContext context) : base(context)
        {
        }


        public void ReplaceAll(Guid encounterId, IEnumerable<ObsPartnerTraceResult> obses)
        {
            var obsPartnerTraceResults = Context.ObsPartnerTraceResults.Where(x => x.EncounterId == encounterId).ToList();
            if (obsPartnerTraceResults.Count > 0)
            {
                Context.RemoveRange(obsPartnerTraceResults);
                Context.SaveChanges();
            }
            Insert(obses);
        }
    }
}
