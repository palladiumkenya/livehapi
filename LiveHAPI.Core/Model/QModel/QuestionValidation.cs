using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.QModel
{
    public class QuestionValidation : Entity<Guid>
    {
        [MaxLength(50)]
        public string ValidatorId { get; set; }
        [MaxLength(50)]
        public string ValidatorTypeId { get; set; }
        public int Revision { get; set; }
        [MaxLength(50)]
        public string MinLimit { get; set; }
        [MaxLength(50)]
        public string MaxLimit { get; set; }
        
        public Guid QuestionId { get; set; }

        public QuestionValidation()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}