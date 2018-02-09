using System;

namespace LiveHAPI.IQCare.Infrastructure.Tests.Maps
{
    public class PreTestBindMap
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
        public int? BindId { get; set; }
        public string Iqfield;
        public PreTestBindMap()
        {
        }

        public bool IsYesNo()
        {
            return  BindTable.Trim().ToLower() == "Mst_YesNo".Trim().ToLower();
        }

        public static string GetQuery()
        {
            return $@"
           SELECT        Questions.Id, SubscriberMaps.Field, Questions.Display, SubscriberMaps.Name, SubscriberMaps.SubName, SubscriberMaps.SubField, SubscriberMaps.Mode,i.BindTable,i.BindID,i.Field as 'Iqfield'
            FROM            Questions INNER JOIN
                                     SubscriberMaps ON CAST(Questions.Id AS varchar(50)) = SubscriberMaps.Field inner join 
									 IQCare.dbo.htchapiall as i on SubscriberMaps.SubField=i.Field and  SubscriberMaps.SubName=i.[Table]
            WHERE        (Questions.Fact <> N'alien') and i.BindTable in ('Mst_ModDecode','Mst_YesNo')
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