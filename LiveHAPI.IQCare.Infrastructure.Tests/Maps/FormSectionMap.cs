namespace LiveHAPI.IQCare.Infrastructure.Tests.Maps
{
    public class FormSectionMap
    {
        public int? Id { get; set; }
        public int? FeatureId { get; set; }
        public int? SectionId { get; set; }

        public static string GetQuery()
        {
            return $@"
                        SELECT DISTINCT 
	                        FeatureId
                        FROM            
	                        [mst_Feature] ";

        }
        public static string GetQuerySection()
        {
            return $@"
                        SELECT DISTINCT 
	                        SectionId
                        FROM            
	                        [mst_Section] ";

        }
    }
}