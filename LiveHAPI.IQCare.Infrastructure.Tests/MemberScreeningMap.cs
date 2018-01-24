using System;

namespace LiveHAPI.IQCare.Infrastructure.Tests
{
    public class MemberScreeningMap
    {
        public Guid Id { get; set; }
        public string Field { get; set; }
        public string Display { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public string SubField { get; set; }
        public string Mode { get; set; }

        public MemberScreeningMap()
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
            WHERE        (Name = N'ObsMemberScreening') and (SubName = N'DTL_CUSTOMFORM_Family Testing Form_FamilyTestingForm') 
            ";

        }
    }
}