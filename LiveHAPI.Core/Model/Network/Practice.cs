using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Studio;
using LiveHAPI.Core.ValueModel;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Network
{
    
    public class Practice : Entity<Guid>, IPractice
    {
        [MaxLength(20)]
        public string Code { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        [MaxLength(50)]
        public string PracticeTypeId { get; set; }
        public int? CountyId { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Provider> Providers { get; set; } = new List<Provider>();
        public ICollection<Client> Clients { get; set; } = new List<Client>();
        public ICollection<PracticeActivation> Activations { get; set; }=new List<PracticeActivation>();
        public ICollection<FormImplementation> FormImplementation { get; set; } = new List<FormImplementation>();

        public Practice()
        {
            Id = LiveGuid.NewGuid();
        }

        public bool IsDeviceActivated(string device)
        {
            return Activations.Any(x => x.Device.ToLower() == device.ToLower() &&  x.IsActive());
        }

        public bool IsDeviceExpired(string device)
        {
            return Activations.Any(x => x.Device.ToLower() == device.ToLower() && x.IsExpired());
        }

        public PracticeActivation ActivateDevice(DeviceIdentity deviceIdentity,DeviceLocation deviceLocation = null)
        {
            if (IsDeviceActivated(deviceIdentity.Serial))
            {
                return Activations.FirstOrDefault(x => x.IsActive());
            }

            if (IsDeviceExpired(deviceIdentity.Serial))
            {
                var expiredDevice=Activations.FirstOrDefault(x => x.IsExpired());
                expiredDevice.Renew(deviceIdentity, deviceLocation);

                return expiredDevice;
            }

            return PracticeActivation.Create(Id, deviceIdentity, deviceLocation);
        }

        public void AddActivation(PracticeActivation activation)
        {
            activation.PracticeId = Id;
            Activations.Add(activation);
        }

        public void AddActivations(IEnumerable<PracticeActivation> activations)
        {
            foreach (var a in activations)
            {
               AddActivation(a);
            }
        }
        public override string ToString()
        {
            return $"{Code} - {Name}";
        }
    }
}