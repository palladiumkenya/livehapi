using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveHAPI.IQCare.Core.Model
{
    [Table("mst_Facility")]
  public  class Location
    {
        [Key]
        public int FacilityID { get; set; }
        public string FacilityName { get; set; }
        public string CountryID { get; set; }
        public string PosID { get; set; }
        public string SatelliteID { get; set; }
        public int DeleteFlag { get; set; }
        

        public override string ToString()
      {
          return $"{FacilityName} ({FacilityID})";
      }
    }
}

/*
FacilityID	int	
FacilityName	varchar(50)	
CountryID	varchar(10)	
PosID	varchar(10)	
SatelliteID	varchar(10)	

SELECT        FacilityID, FacilityName, CountryID, PosID, SatelliteID
FROM            mst_Facility
*/
