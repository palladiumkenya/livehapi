using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ObsPartnerScreeningRepository : BaseRepository<ObsPartnerScreening, Guid> , IObsPartnerScreeningRepository
    {
        public ObsPartnerScreeningRepository(LiveHAPIContext context) : base(context)
        {
        }


        public void ReplaceAll(Guid encounterId, IEnumerable<ObsPartnerScreening> obses)
        {
            var obsPartnerScreenings = Context.ObsPartnerScreenings.Where(x => x.EncounterId == encounterId).ToList();
            if (obsPartnerScreenings.Count > 0)
            {
                Context.RemoveRange(obsPartnerScreenings);
                Context.SaveChanges();
            }
            Insert(obses);
        }
    }
}
