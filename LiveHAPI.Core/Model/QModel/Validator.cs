using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.QModel
{
    public class Validator:Entity<string>
    {
        [MaxLength(50)]
        public string Name { get; set; }
        public decimal Rank { get; set; }
        public ICollection<QuestionValidation> QuestionValidations { get; set; } = new List<QuestionValidation>();
    }
}