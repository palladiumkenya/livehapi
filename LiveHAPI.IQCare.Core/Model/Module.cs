using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveHAPI.IQCare.Core.Model
{
    // ReSharper disable once InconsistentNaming

  [Table("mst_module")]
  public  class Module
    {
        [Key]
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public bool DeleteFlag { get; set; }
        public ICollection<Feature> Features { get; set; }

      public Module()
      {
            Features=new  List<Feature>();
      }

      public void AddFeature(Feature feature)
      {
          feature.Module = this;
        Features.Add(feature);
      }
        public void AddFeatures(IEnumerable<Feature> features)
        {
            foreach (var f in features)
            {
                AddFeature(f);
            }
        }

      public override string ToString()
      {
          return $"{ModuleName} ({ModuleID})";
      }
    }
}

/*
ModuleID	int	Unchecked
ModuleName	varchar(50)	Checked
DeleteFlag	int	Checked
UserId	int	Checked
CreateDate	datetime	Checked
UpdateDate	datetime	Checked
Status	int	Checked
UpdateFlag	int	Checked
Identifier	int	Checked
PharmacyFlag	int	Checked
Unchecked

SELECT        ModuleID, ModuleName, DeleteFlag, UserId, CreateDate, UpdateDate, Status, UpdateFlag, Identifier, PharmacyFlag
FROM            mst_module
*/
