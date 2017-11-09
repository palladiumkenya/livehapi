using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ObsFamilyTraceResultRepository : BaseRepository<ObsFamilyTraceResult, Guid> , IObsFamilyTraceResultRepository
    {
        public ObsFamilyTraceResultRepository(LiveHAPIContext context) : base(context)
        {
        }


        public void ReplaceAll(Guid encounterId, IEnumerable<ObsFamilyTraceResult> obses)
        {
            var obsFamilyTraceResults = Context.ObsFamilyTraceResults.Where(x => x.EncounterId == encounterId).ToList();
            if (obsFamilyTraceResults.Count > 0)
            {
                Context.RemoveRange(obsFamilyTraceResults);
                Context.SaveChanges();
            }
            Insert(obses);
        }
    }
}
