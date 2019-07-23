using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagerAPI.Features.Task
{
    public class TaskModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public int priority { get; set; }

        public int? parentTaskId { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public int userId { get; set; }

        public int projectId { get; set; }

        public TaskModel parentTask { get; set; }

        public bool isCompleted { get; set; }

        public bool isParentTask { get; set; }
    }
}