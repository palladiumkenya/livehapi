using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
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