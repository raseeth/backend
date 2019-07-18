using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerAPI.Features.Project
{
    public interface IProjectService
    {
        void CreateProject(ProjectModel project);

        void DeleteProject(int id);

        ProjectModel GetProjectById(int id);

        IList<ProjectModel> GetProjects();

        void UpdateProject(int id, ProjectModel project);
    }
}
