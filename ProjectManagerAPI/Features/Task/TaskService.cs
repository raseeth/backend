using ProjectManagerAPI.Infrastructure;
using ProjectManagerAPI.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagerAPI.Features.Task
{
    public class TaskService : ITaskService
    {
        private readonly IProjectManagerDBContext dbContext;

        public TaskService(IProjectManagerDBContext projectManagerDBContext)
        {
            this.dbContext = projectManagerDBContext;
        }

        public void CreateTask(TaskModel task)
        {
            var taskEntity = new TaskEntity()
            {
               Name = task.name,
               IsParentTask = task.isParentTask,
               UserId = task.userId,
               StartDate = task.startDate,
               ParentTaskId = task.parentTaskId,
               EndDate = task.endDate,
               IsCompleted = task.isCompleted,
               Priority = task.priority,
               ProjectId = task.projectId
            };

            this.dbContext.Tasks.Add(taskEntity);
            this.dbContext.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            this.dbContext.Tasks.Remove(this.dbContext.Tasks.FirstOrDefault(x => x.Id == id));
            this.dbContext.SaveChanges();
        }

        public TaskModel GetTaskById(int id)
        {
            var task = this.dbContext.Tasks.FirstOrDefault(x => x.Id == id);

            return Map(task);
        }

        public IList<TaskModel> GetTasks()
        {
            return this.dbContext.Tasks.ToList().Select(x => Map(x)).ToList();
        }

        public void UpdateTask(int id, TaskModel task)
        {
            var taskEntity = this.dbContext.Tasks.FirstOrDefault(x => x.Id == id);

            if (taskEntity != null)
            {
                taskEntity.ProjectId = task.projectId;
                taskEntity.StartDate = task.startDate;
                taskEntity.UserId = task.userId;
                taskEntity.ParentTaskId = task.parentTaskId == 0 ? null : task.parentTaskId;
                taskEntity.IsParentTask = task.isParentTask;
                taskEntity.IsCompleted = task.isCompleted;
                taskEntity.ProjectId = task.projectId;
                taskEntity.Priority = task.priority;
                taskEntity.EndDate = task.endDate;
                taskEntity.Name = task.name;
            }

            this.dbContext.SaveChanges();
        }

        private static TaskModel Map(TaskEntity taskEntity)
        {
            return new TaskModel()
            {
                endDate = taskEntity.EndDate,
                id = taskEntity.Id,
                isCompleted = taskEntity.IsCompleted,
                name = taskEntity.Name,
                parentTaskId = taskEntity.ParentTaskId ?? 0,
                isParentTask = taskEntity.IsParentTask || taskEntity.ParentTask != null,
                projectId = taskEntity.ProjectId,
                priority = taskEntity.Priority,
                parentTask = taskEntity.ParentTask != null ? Map(taskEntity.ParentTask) : new TaskModel(),
                startDate = taskEntity.StartDate,
                userId = taskEntity.UserId
            };
        }
    }
}