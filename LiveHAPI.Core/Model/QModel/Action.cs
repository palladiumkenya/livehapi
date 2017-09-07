using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.QModel
{
    public class Action:Entity<string>
    {
        [Key]
        [MaxLength(50)]
        public override string Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<QuestionBranch> Branches { get; set; } = new List<QuestionBranch>();

        public ICollection<QuestionReValidation> ReValidations { get; set; } = new List<QuestionReValidation>();
        public ICollection<QuestionTransformation> Transformations { get; set; } = new List<QuestionTransformation>();
        public ICollection<QuestionRemoteTransformation> RemoteTransformations { get; set; } = new List<QuestionRemoteTransformation>();
    }
}