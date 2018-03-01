using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Infrastructure.Repository
{
  public  class EncounterRepository : BaseRepository<Encounter, Guid> ,IEncounterRepository
    {
        public EncounterRepository(LiveHAPIContext context) : base(context)
        {
        }

        public List<Encounter> LoadEncounters(Guid clientId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Encounter> LoadByUser(Guid userId)
        {
            /*
                HTS Initial             7e5164a6-6b99-11e7-907b-a6006ad3dba0
                HTS Initial Test        b262f4ee-852f-11e7-bb31-be2e44b06b34
                HTS Confirmatory Test   b262faac-852f-11e7-bb31-be2e44b06b34
                HTS Linkage             b262fc32-852f-11e7-bb31-be2e44b06b34
                HTS Trace               b262fda4-852f-11e7-bb31-be2e44b06b34
                Family Screening        b262fda4-877f-11e7-bb31-be2e44b66b34
                Family Trace            b262fda4-877f-11e7-bb31-be2e44b67b34
                Partner Screening       b262fda4-877f-11e7-bb31-be2e44b68b34
                Partner Trace           b262fda4-877f-11e7-bb31-be2e44b69b34
             */

            var encounters = Context.Encounters
                .Where(x => x.UserId == userId&&x.EncounterTypeId==new Guid("b262fda4-877f-11e7-bb31-be2e44b68b34"))
                .Include(x => x.ObsPartnerScreenings);

            return encounters;
        }
    }
}
