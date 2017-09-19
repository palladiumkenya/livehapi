using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveHAPI.IQCare.Core.Model
{
    // ReSharper disable InconsistentNaming

    [Table("mst_VisitType")]
  public  class VisitType
    {
        [Key]
        public int VisitTypeID { get; set; }
        public string VisitName { get; set; }
        public int DeleteFlag { get; set; }
        public int? SystemId { get; set; }
        [ForeignKey("Feature")]
        public int? FeatureId { get; set; }

        public virtual Feature Feature { get; set; }

        public override string ToString()
        {
            return $"{VisitName} ({VisitTypeID})";
        }

        public string ToStringDetail()
        {
            var fmtString = string.Empty;
            if (null != Feature)
            {
                if (null != Feature.Module)
                {
                    fmtString = $"{Feature.Module}>{Feature}>";
                }
                else
                {
                    fmtString = $"[]>{Feature}>";
                }
            }
            return $"{fmtString}{this}";
        }
    }
}

/*
VisitTypeID	int	
VisitName	varchar(50)	
DeleteFlag	int	
UserID	int	
CreateDate	datetime
UpdateDate	datetime
SystemId	int
FeatureId	int

SELECT        VisitTypeID, VisitName, DeleteFlag, UserID, CreateDate, UpdateDate, SystemId, FeatureId
FROM            mst_VisitType

     */
