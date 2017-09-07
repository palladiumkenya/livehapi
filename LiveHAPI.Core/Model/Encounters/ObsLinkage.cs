using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Encounters
{
    public class ObsLinkage : Entity<Guid>
    {
        [MaxLength(50)]
        public string ReferredTo { get; set; }

        public DateTime? DatePromised { get; set; }
        [MaxLength(50)]
        public string FacilityHandedTo { get; set; }
        [MaxLength(50)]
        public string HandedTo { get; set; }
        [MaxLength(50)]
        public string WorkerCarde { get; set; }
        public DateTime? DateEnrolled { get; set; }
        [MaxLength(50)]
        public string EnrollmentId { get; set; }
        [MaxLength(50)]
        public string Remarks { get; set; }
        public Guid EncounterId { get; set; }

        public ObsLinkage()
        {
            Id = LiveGuid.NewGuid();
        }

    
    }
}