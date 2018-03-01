using System;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class ObsPartnerScreeningInfo :IObsPartnerScreening
    {
        public Guid Id { get; set; }
        public DateTime ScreeningDate { get; set; }
        public Guid IPVScreening { get; set; }
        public Guid PhysicalAssult { get; set; }
        public Guid Threatened { get; set; }
      
        public Guid SexuallyUncomfortable { get; set; }
        public Guid HivStatus { get; set; }
        public Guid Eligibility { get; set; }

        public Guid IndexClientId { get; set; }

        public Guid? PNSApproach { get; set; }
        public Guid? LivingWithClient { get; set; }
        public Guid? PNSRealtionship { get; set; }
        public string Occupation { get; set; }
        public Guid? IPVOutcome { get; set; }
        public Guid? PnsAccepted { get; set; }
        public DateTime? BookingDate { get; set; }
        public string Remarks { get; set; }
        public string PhoneNumber { get; set; }
        public Guid EncounterId { get; set; }
        public bool BookingMet { get; set; }
        public DateTime? DateBookingMet { get; set; }
        public Guid? TraceId { get; set; }
    }
}
