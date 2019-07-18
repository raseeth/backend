using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectManagerAPI.Features.Task
{
    public class TaskController : ApiController
    {
        private readonly ITaskService service;

        public TaskController(ITaskService taskService)
        {
            this.service = taskService;
        }

        public IEnumerable<TaskModel> Get()
        {
            return this.service.GetTasks();
        }

        public TaskModel Get(int id)
        {
            return this.service.GetTaskById(id);
        }

        public void Post([FromBody]TaskModel value)
        {
            this.service.CreateTask(value);
        }

        public void Put(int id, [FromBody]TaskModel value)
        {
            this.service.UpdateTask(id, value);
        }

        public void Delete(int id)
        {
            this.service.DeleteTask(id);
        }
    }
}
