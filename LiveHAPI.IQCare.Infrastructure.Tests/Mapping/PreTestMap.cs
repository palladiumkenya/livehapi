using System;

namespace LiveHAPI.IQCare.Infrastructure.Tests.Mapping
{
    public class PreTestMap
    {
        public Guid Id { get; set; }
        public string Field { get; set; }
        public string Display { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public string SubField { get; set; }
        public string Mode { get; set; }
        public string Fact { get; set; }

        public PreTestMap()
        {
        }

        public string SqlColumn()
        {
            return $"SELECT count({SubField}) FROM {SubName}";
        }
        public string Info()
        {
            return $"{Display} > {SubName}.{SubField}";
        }
    }
}