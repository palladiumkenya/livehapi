using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.ValueModel;
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
        private PracticeActivation(string device, string model, string deviceCode) : this()
        {
            Device = device;
            Model = model;
            DeviceCode = deviceCode;
        }
        private PracticeActivation(string device, string model, string deviceCode, string ipAddress, decimal? lng, decimal? lat):this(device,  model,  deviceCode)
        {
            IPAddress = ipAddress;
            Lng = lng;
            Lat = lat;
        }

     
        public static PracticeActivation Create(DeviceIdentity deviceIdentity, DeviceLocation deviceLocation = null,bool activate=true)
        {
            PracticeActivation activation = null;

            if (null != deviceLocation)
            {
                activation = new PracticeActivation(
                    deviceIdentity.Serial, deviceIdentity.Model, deviceIdentity.Code,
                    deviceLocation.IPAddress,deviceLocation.Lng, deviceLocation.Lat);
            }
            else
            {
                activation = new PracticeActivation(deviceIdentity.Serial, deviceIdentity.Model, deviceIdentity.Code);
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

        internal void Renew(DeviceIdentity deviceIdentity, DeviceLocation deviceLocation=null)
        {
            Update(deviceIdentity,deviceLocation);
            Activate();
        }

        private void Update(DeviceIdentity deviceIdentity, DeviceLocation deviceLocation = null)
        {
            Model = deviceIdentity.Model;
            DeviceCode = deviceIdentity.Code;

            if (null != deviceLocation)
            {
                IPAddress = deviceLocation.IPAddress;
                Lng = deviceLocation.Lng;
                Lat = deviceLocation.Lat;
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