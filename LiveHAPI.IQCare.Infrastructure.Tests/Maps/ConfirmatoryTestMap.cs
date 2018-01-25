using System;

namespace LiveHAPI.IQCare.Infrastructure.Tests.Maps
{
    public class ConfirmatoryTestMap
    {
        public Guid Id { get; set; }
        public string Field { get; set; }
        public string Display { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public string SubField { get; set; }
        public string Mode { get; set; }

        public ConfirmatoryTestMap()
        {
        }

        public string SqlColumn()
        {
            return $"SELECT count([{SubField}]) FROM [{SubName}]";
        }
        public string Info()
        {
            return $"{Display} > {SubName}.{SubField}";
        }

        public static string GetQuery()
        {
            return  $@"
            SELECT        Id, SubscriberMaps.Field, SubscriberMaps.Field as Display, SubscriberMaps.Name, SubscriberMaps.SubName, SubscriberMaps.SubField, SubscriberMaps.Mode
            FROM            
			SubscriberMaps 
            WHERE        (Name = N'ObsTestResult') and (SubName = N'DTL_CUSTOMFORM_HIV-Test 2_HTC_Lab_MOH_362') 
            ";

        }
    }
}