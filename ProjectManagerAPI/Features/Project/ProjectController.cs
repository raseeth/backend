using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectManagerAPI.Features.Project
{
    public class ProjectController : ApiController
    {
        private readonly IProjectService service;

        public ProjectController(IProjectService projectService)
        {
            this.service = projectService;
        }

        public IEnumerable<ProjectModel> Get()
        {
            return this.service.GetProjects();
        }

        public ProjectModel Get(int id)
        {
            return this.service.GetProjectById(id);
        }

        public void Post([FromBody]ProjectModel value)
        {
            this.service.CreateProject(value);
        }

        public void Put(int id, [FromBody]ProjectModel value)
        {
            this.service.UpdateProject(id, value);
        }

        public void Delete(int id)
        {
            this.service.DeleteProject(id);
        }
    }
}
