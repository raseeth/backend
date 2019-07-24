using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagerAPI.Infrastructure.Entities
{
    public class ProjectEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Priority { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [ForeignKey("User")]
        public virtual int UserId { get; set; }

        public virtual UserEntity User { get; set; }

        public virtual ICollection<TaskEntity> Tasks { get; set; }
    }
}