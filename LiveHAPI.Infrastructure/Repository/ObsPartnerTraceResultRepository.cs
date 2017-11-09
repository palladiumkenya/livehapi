using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;
using Microsoft.EntityFrameworkCore;

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

        public void UpdateBooking(Encounter encounter, ObsPartnerTraceResult familyTraceResult)
        {
            if (null != encounter)
            {
                var encounterToUpdate = Context.Encounters.Include(x => x.ObsPartnerScreenings)
                    .FirstOrDefault(e => e.ClientId == encounter.ClientId);


                if (null != encounterToUpdate && encounterToUpdate.ObsPartnerScreenings.Count > 0)
                {
                    var booking = encounterToUpdate.ObsPartnerScreenings.FirstOrDefault(x => x.BookingMet == false);
                    if (null != booking)
                    {
                        booking.BookingMet = true;
                        booking.TraceId = familyTraceResult.Id;
                        booking.DateBookingMet = familyTraceResult.Date;
                        Context.ObsPartnerScreenings.Attach(booking);
                        Context.Entry(booking).State = EntityState.Modified;
                        Context.SaveChanges();
                    }
                }

            }
        }
    }
}
