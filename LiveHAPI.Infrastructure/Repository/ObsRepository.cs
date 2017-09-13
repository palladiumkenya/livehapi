using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ObsRepository : BaseRepository<Obs, Guid> ,IObsRepository
    {
        public ObsRepository(LiveHAPIContext context) : base(context)
        {
        }


        public void ReplaceAll(Guid encounterId, IEnumerable<Obs> obses)
        {
            var existingObses = Context.Obses.Where(x => x.EncounterId == encounterId).ToList();
            if (existingObses.Count > 0)
            {
                Context.RemoveRange(existingObses);
                Context.SaveChanges();
            }
            Insert(obses);
        }
    }
}
