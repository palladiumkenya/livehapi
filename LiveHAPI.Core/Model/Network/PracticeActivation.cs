using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Network
{
    public class PracticeActivation: Entity<Guid>
    {
        [MaxLength(150)]
        public string Device { get; set; }
        [MaxLength(150)]
        public string Model { get; set; }
        [MaxLength(150)]
        public string DeviceCode { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ActivationDate { get; set; }
        [MaxLength(50)]
        public string ActivationCode { get; set; }
        public DateTime? ExpiryDate { get; set; }
        [MaxLength(150)]
        public string IPAddress { get; set; }
        public decimal? Lng { get; set; }
        public decimal? Lat { get; set; }
        [MaxLength(150)]
        public string Notes { get; set; }
        public Guid PracticeId { get; set; }

        public PracticeActivation()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}