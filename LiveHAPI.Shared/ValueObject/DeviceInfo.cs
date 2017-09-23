using System;

namespace LiveHAPI.Shared.ValueObject
{
    public class DeviceInfo
    {
        public string Serial { get; set; }
        public string Model { get; set; }
        public string Code { get; set; }

        public DeviceInfo()
        {
        }

        public DeviceInfo(string serial, string model, string code)
        {
            Serial = serial ?? throw new ArgumentNullException(nameof(serial));
            Model = model;
            Code = code ?? throw new ArgumentNullException(nameof(code));
        }

        public override string ToString()
        {
            return $"{Serial}-{Model}-{Code}";
        }
    }
}