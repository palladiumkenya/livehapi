using System;

namespace LiveHAPI.Core.ValueModel
{
    public class DeviceIdentity
    {
        public string Serial { get; set; }
        public string Model { get; set; }
        public string Code { get; set; }

        public DeviceIdentity()
        {
        }

        public DeviceIdentity(string serial, string model, string code)
        {
            Serial = serial ?? throw new ArgumentNullException(nameof(serial));
            Model = model;
            Code = code ?? throw new ArgumentNullException(nameof(code));
        }
    }
}