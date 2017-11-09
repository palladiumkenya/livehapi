using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ObsMemberScreeningRepository : BaseRepository<ObsMemberScreening, Guid> , IObsMemberScreeningRepository
    {
        public ObsMemberScreeningRepository(LiveHAPIContext context) : base(context)
        {
        }


        public void ReplaceAll(Guid encounterId, IEnumerable<ObsMemberScreening> obses)
        {
            var obsMemberScreenings = Context.ObsMemberScreenings.Where(x => x.EncounterId == encounterId).ToList();
            if (obsMemberScreenings.Count > 0)
            {
                Context.RemoveRange(obsMemberScreenings);
                Context.SaveChanges();
            }
            Insert(obses);
        }
    }
}
