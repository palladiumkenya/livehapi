using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;
using Microsoft.EntityFrameworkCore;

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

        public void UpdateBooking(Encounter encounter, ObsFamilyTraceResult familyTraceResult)
        {
          
            if (null != encounter)
            {
                var encounterToUpdate = Context.Encounters.Include(x => x.ObsMemberScreenings)
                    .FirstOrDefault(e => e.ClientId == encounter.ClientId);
                 

                if (null!= encounterToUpdate&&encounterToUpdate.ObsMemberScreenings.Count>0)
                {
                    var booking = encounterToUpdate.ObsMemberScreenings.FirstOrDefault(x=>x.BookingMet==false);
                    if (null != booking)
                    {
                        booking.BookingMet = true;
                        booking.TraceId = familyTraceResult.Id;
                        booking.DateBookingMet = familyTraceResult.Date;
                        Context.ObsMemberScreenings.Attach(booking);
                        Context.Entry(booking).State = EntityState.Modified;
                        Context.SaveChanges();
                    }
                }

            }
        }
   }
}
