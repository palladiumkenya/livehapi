using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ObsTraceResultRepository : BaseRepository<ObsTraceResult, Guid> ,IObsTraceResultRepository
    {
        public ObsTraceResultRepository(LiveHAPIContext context) : base(context)
        {
        }


        public void ReplaceAll(Guid encounterId, IEnumerable<ObsTraceResult> obses)
        {
            var existingObsTraceResultes = Context.ObsTraceResults.Where(x => x.EncounterId == encounterId).ToList();
            if (existingObsTraceResultes.Count > 0)
            {
                Context.RemoveRange(existingObsTraceResultes);
                Context.SaveChanges();
            }
            Insert(obses);
        }
    }
}
