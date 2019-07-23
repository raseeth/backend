using ProjectManagerAPI.Features.User;
using System;

namespace ProjectManagerAPI.Features.Project
{
    public class ProjectModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int priority { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int userId { get; set; }
        public int tasks { get; set; }
        public int completedTasks { get; set; }
        public UserModel user { get; set; }
    }
}