using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Encounters
{
    public class ObsTestResult : Entity<Guid>
    {
        [MaxLength(50)]
        public string TestName { get; set; }
        public int Attempt {get; set; }
        
        public Guid Kit { get; set; }
        [MaxLength(50)]
        public string KitDisplay { get; set; }
        [MaxLength(50)]
        public string KitOther { get; set; }
        [MaxLength(50)]
        public string LotNumber { get; set; }
        public DateTime Expiry { get; set; }
        
        public Guid Result { get; set; }
        [MaxLength(50)]
        public string ResultCode { get; set; }

        [MaxLength(50)]
        public string ResultDisplay { get; set; }
        public bool IsValid { get; set; }
        
        public Guid EncounterId { get; set; }

        public ObsTestResult()
        {
            Id = LiveGuid.NewGuid();
        }

      

    }
}