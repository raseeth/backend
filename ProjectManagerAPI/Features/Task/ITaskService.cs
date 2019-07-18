using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerAPI.Features.Task
{
    public interface ITaskService
    {
        void CreateTask(TaskModel task);

        void DeleteTask(int id);

        TaskModel GetTaskById(int id);

        IList<TaskModel> GetTasks();

        void UpdateTask(int id, TaskModel task);
    }
}
