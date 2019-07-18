using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ProjectManagerAPI.Features.Project;
using ProjectManagerAPI.Features.Task;
using ProjectManagerAPI.Features.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ProjectManagerAPI.Installers
{
    public class WebAPIInstallers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>()
                .LifestyleTransient());
            container.Register(Component.For<ITaskService>()
                .ImplementedBy<TaskService>().LifestyleTransient());
            container.Register(Component.For<IUserService>()
                .ImplementedBy<UserService>().LifestyleTransient());
            container.Register(Component.For<IProjectService>()
                .ImplementedBy<ProjectService>().LifestyleTransient());
        }
    }
}