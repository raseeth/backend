using ProjectManagerAPI.Infrastructure;
using ProjectManagerAPI.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagerAPI.Features.Project
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectManagerDBContext dbContext;

        public ProjectService(IProjectManagerDBContext projectManagerDBContext)
        {
            this.dbContext = projectManagerDBContext;
        }

        public void CreateProject(ProjectModel project)
        {
            var entity = new ProjectEntity()
            {
                EndDate = project.endDate,
                StartDate = project.startDate,
                Name = project.name,
                Priority = project.priority,
                UserId = project.userId
            };
            this.dbContext.Projects.Add(entity);
            this.dbContext.SaveChanges();
        }

        public void DeleteProject(int id)
        {
            this.dbContext.Projects.Remove(this.dbContext.Projects.FirstOrDefault(x => x.Id == id));
            this.dbContext.SaveChanges();
        }

        public ProjectModel GetProjectById(int id)
        {
            var project = this.dbContext.Projects.FirstOrDefault(x => x.Id == id);

            return Map(project);
        }

        public IList<ProjectModel> GetProjects()
        {
            return this.dbContext.Projects.ToList().Select(x => Map(x)).ToList();
        }

        public void UpdateProject(int id, ProjectModel project)
        {
            var projectEntity = this.dbContext.Projects.FirstOrDefault(x => x.Id == id);

            if (projectEntity != null)
            {
                projectEntity.Name = project.name;
                projectEntity.Priority = project.priority;
                projectEntity.StartDate = project.startDate;
                projectEntity.EndDate = project.endDate;
                projectEntity.UserId = project.userId;
            }

            this.dbContext.SaveChanges();
        }

        private static ProjectModel Map(ProjectEntity project)
        {
            var response = new ProjectModel();
            if (project != null)
            {
                response.name = project.Name;
                response.id = project.Id;
                response.priority = project.Priority;
                response.startDate = project.StartDate;
                response.endDate = project.EndDate;
                response.userId = project.UserId;
                response.user = new User.UserModel();
                if (project.User != null)
                {
                    response.user.employeeId = project.User.EmployeeId;
                    response.user.firstName = project.User.FirstName;
                    response.user.lastName = project.User.LastName;
                    response.user.id = project.User.Id;
                }
                if (project.Tasks != null)
                {
                    response.tasks = project.Tasks.Count();
                    response.completedTasks = project.Tasks.Count(x => x.IsCompleted == true);
                }
            }
            return response;
        }
    }
}