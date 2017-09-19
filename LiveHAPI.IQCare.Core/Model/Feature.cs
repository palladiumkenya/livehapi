using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveHAPI.IQCare.Core.Model
{
    // ReSharper disable once InconsistentNaming
    [Table("mst_Feature")]
  public  class Feature
    {
        [Key]
        public int FeatureID { get; set; }
        public string FeatureName { get; set; }
        public int DeleteFlag { get; set; }
        public int SystemId { get; set; }
        [ForeignKey("Module")]
        public int ModuleId { get; set; }

        public virtual Module Module { get; set; }
        public virtual ICollection<VisitType> VisitTypes { get; set; }

        public Feature()
        {
            VisitTypes=new List<VisitType>();
        }

        public void AddVisitType(VisitType visitType)
        {
            visitType.Feature = this;
            VisitTypes.Add(visitType);
        }
        public void AddVisitTypes(IEnumerable<VisitType> visitTypes)
        {
            foreach (var v in visitTypes)
            {
                AddVisitType(v);
            }
        }
        public override string ToString()
        {
            return $"{FeatureName} ({FeatureID})";
        }

        /*
FeatureID	int	Unchecked
FeatureName	varchar(50)	Checked
ReportFlag	int	Checked
DeleteFlag	int	Checked
AdminFlag	int	Checked
UserID	int	Checked
CreateDate	datetime	Checked
UpdateDate	datetime	Checked
OptionalFlag	int	Checked
SystemId	int	Checked
Published	int	Checked
CountryId	int	Checked
ModuleId	int	Checked
MultiVisit	int	Checked
Seq	int	Checked
RegistrationFormFlag	int	Checked
		Unchecked
		

SELECT        FeatureID, FeatureName, ReportFlag, DeleteFlag, AdminFlag, UserID, CreateDate, UpdateDate, OptionalFlag, SystemId, Published, CountryId, ModuleId, MultiVisit, Seq, RegistrationFormFlag
FROM            mst_Feature
        */
    }
}
