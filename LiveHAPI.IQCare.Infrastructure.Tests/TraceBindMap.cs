using System;

namespace LiveHAPI.IQCare.Infrastructure.Tests
{
    public class TraceBindMap
    {
      
        public Guid Id { get; set; }
        public string Field { get; set; }
        public string Display { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public string SubField { get; set; }
        public string Mode { get; set; }
        public string Fact { get; set; }
        public string BindTable { get; set; }

        public string TranslationField => $"{Name}.{Field}";
        public int? BindId { get; set; }
        public string Iqfield;
        public TraceBindMap()
        {
        }

        public bool IsYesNo()
        {
            return  BindTable.Trim().ToLower() == "Mst_YesNo".Trim().ToLower();
        }

        public static string GetQuery()
        {
            return $@"
           SELECT        Id, SubscriberMaps.Field, SubscriberMaps.Field as Display, SubscriberMaps.Name, SubscriberMaps.SubName, SubscriberMaps.SubField, SubscriberMaps.Mode,i.BindTable,i.BindID,i.Field as 'Iqfield'
            FROM            
			SubscriberMaps 
			inner join 
									 IQCare.dbo.htchapiall as i on SubscriberMaps.SubField=i.Field and  SubscriberMaps.SubName=i.[Table]

            WHERE        (Name = N'ObsTraceResult') and (SubName = N'DTL_CUSTOMFORM_HTS Tracing_LinkageAndTracking')
			and i.BindTable in ('Mst_ModDecode','Mst_YesNo')

            ";
        }

        public string GetLookups()
        {
            string sql= $@"SELECT * from [{BindTable}] ";
            if (BindId.HasValue)
                return $@"{sql} where [codeid]={BindId.Value}";
            return sql;
        }
    }
}