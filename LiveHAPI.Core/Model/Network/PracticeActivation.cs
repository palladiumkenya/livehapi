using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Identifier { get; set; }
        [NotMapped]
        public string IdentifierPrefix
        {
            get
            {
                var idLength = Identifier.ToString().Length;

                if (idLength > 3)
                    return $"A{Identifier.ToString($"D{idLength}")}";

                return $"A{Identifier:D3}";
            }
        }

        public Guid PracticeId { get; set; }

        public bool IsActive()
        {
            return !string.IsNullOrWhiteSpace(ActivationCode) &&
                   ExpiryDate.HasValue &&
                   ExpiryDate >= DateTime.Today.Date;
        }

        public bool IsExpired()
        {
            return !string.IsNullOrWhiteSpace(ActivationCode) &&
                   ExpiryDate.HasValue &&
                   ExpiryDate < DateTime.Today.Date;
        }

        public PracticeActivation()
        {
            Id = LiveGuid.NewGuid();
        }
        private PracticeActivation(Guid practiceId, string device, string model) : this()
        {
            Device = device;
            Model = model;
            PracticeId = practiceId;
        }

        private PracticeActivation(Guid practiceId, string device, string model, string deviceCode) : this(practiceId,
            device, model)
        {
            DeviceCode = deviceCode;
        }

        private PracticeActivation(Guid practiceId, string device, string model, string deviceCode, string ipAddress, decimal? lng, decimal? lat):this(practiceId,device,  model,  deviceCode)
        {
            IPAddress = ipAddress;
            Lng = lng;
            Lat = lat;
        }

        public static PracticeActivation Create(DeviceInfo deviceInfo, bool activate = true)
        {
            PracticeActivation activation = null;
            
            activation = new PracticeActivation(deviceInfo.PracticeId, deviceInfo.Serial, deviceInfo.Model);

            if (activate)
                activation.Activate();

            return activation;
        }


        public static PracticeActivation Create(Guid practiceId, DeviceInfo deviceInfo, DeviceLocationInfo deviceLocationInfo = null,bool activate=true)
        {
            PracticeActivation activation = null;

            if (null != deviceLocationInfo)
            {
                activation = new PracticeActivation(practiceId,
                    deviceInfo.Serial, deviceInfo.Model, deviceInfo.Code,
                    deviceLocationInfo.IPAddress,deviceLocationInfo.Lng, deviceLocationInfo.Lat);
            }
            else
            {
                activation = new PracticeActivation(practiceId, deviceInfo.Serial, deviceInfo.Model, deviceInfo.Code);
            }

            if (activate)
                activation.Activate();

            return activation;
        }

       
        private void Activate()
        {
            RequestDate = DateTime.Now;
            ActivationCode = LiveGuid.NewGuid().ToString();
            ActivationDate = DateTime.Now;
            ExpiryDate = DateTime.Now.AddYears(1);
        }

        internal void Renew(DeviceInfo deviceInfo, DeviceLocationInfo deviceLocationInfo=null)
        {
            Update(deviceInfo,deviceLocationInfo);
            Activate();
        }

        private void Update(DeviceInfo deviceInfo, DeviceLocationInfo deviceLocationInfo = null)
        {
            Model = deviceInfo.Model;
            DeviceCode = deviceInfo.Code;

            if (null != deviceLocationInfo)
            {
                IPAddress = deviceLocationInfo.IPAddress;
                Lng = deviceLocationInfo.Lng;
                Lat = deviceLocationInfo.Lat;
            }
            else
            {
                IPAddress = string.Empty;
                Lng = Lat = null;
            }
        }

        public override string ToString()
        {
            return $"{Device}-{Model}-{DeviceCode}|{ActivationCode}|{IsActive()}";
        }
    }
}