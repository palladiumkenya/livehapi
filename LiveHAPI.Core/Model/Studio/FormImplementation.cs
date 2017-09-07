using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Studio
{
    public class FormImplementation:Entity<Guid>
    {
        [MaxLength(50)]
        public string Display { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public Guid PracticeId { get; set; }
        public Guid FormId { get; set; }
        
        public FormImplementation()
        {
            Id = LiveGuid.NewGuid();
        }
        public override string ToString()
        {
            return $"{Display}";
        }
    }
}