using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ObsFinalTestResultRepository : BaseRepository<ObsFinalTestResult, Guid> ,IObsFinalTestResultRepository
    {
        public ObsFinalTestResultRepository(LiveHAPIContext context) : base(context)
        {
        }


        public void ReplaceAll(Guid encounterId, IEnumerable<ObsFinalTestResult> obses)
        {
            var existingObsFinalTestResultes = Context.ObsFinalTestResults.Where(x => x.EncounterId == encounterId).ToList();
            if (existingObsFinalTestResultes.Count > 0)
            {
                Context.RemoveRange(existingObsFinalTestResultes);
                Context.SaveChanges();
            }
            Insert(obses);
        }
    }
}
