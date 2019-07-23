using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagerAPI.Infrastructure.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }

        public bool IsParentTask { get; set; }

        public string Name { get; set; }

        public int Priority { get; set; }

        [ForeignKey("ParentTask")]
        public int? ParentTaskId { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual UserEntity User { get; set; }

        public virtual TaskEntity ParentTask { get; set; }

        public virtual ProjectEntity Project { get; set; }
    }
}