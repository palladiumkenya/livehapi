using System;
using System.ComponentModel.DataAnnotations;

namespace LiveHAPI.IQCare.Core.Model
{
    public class Encounter
    {
        [Key]
        public int Id { get; set; }
        public int VisitId { get; set; }
        public int PatientId { get; set; }
        public int LocationId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int UserId { get; set; }
        public Guid? mAfyaId { get; set; }
    }
}