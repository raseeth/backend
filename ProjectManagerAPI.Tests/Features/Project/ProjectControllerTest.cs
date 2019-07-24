using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using ProjectManagerAPI.Features.Project;

namespace ProjectManagerAPI.Tests.Features.Project
{
    [TestFixture]
    public class ProjectControllerTest
    {
        private ProjectController subject;
        private Mock<IProjectService> mockService;

        public ProjectControllerTest()
        {
            this.mockService = new Mock<IProjectService>();
        }

        [Test]
        public void ProjectControllerGetShouldCallServiceGetAsExpected()
        {
            subject = new ProjectController(this.mockService.Object);

            subject.Get();

            this.mockService.Verify(x => x.GetProjects(), Times.Once);
        }

        [Test]
        public void ProjectControllerGetByIdhouldCallServiceGetByIdAsExpected()
        {
            var request = 123;
            subject = new ProjectController(this.mockService.Object);

            subject.Get(request);

            this.mockService.Verify(x => x.GetProjectById(request), Times.Once);
        }

        [Test]
        public void ProjectControllerPostShouldCallCreateProjectServiceAsExpected()
        {
            var request = GetProjectModel();
            subject = new ProjectController(this.mockService.Object);

            subject.Post(request);

            this.mockService.Verify(x => x.CreateProject(request), Times.Once);
        }

        [Test]
        public void ProjectControllerPutShouldCallUpdateProjectServiceAsExpected()
        {
            var request = GetProjectModel();
            subject = new ProjectController(this.mockService.Object);

            subject.Put(request.id, request);

            this.mockService.Verify(x => x.UpdateProject(request.id, request), Times.Once);
        }

        [Test]
        public void ProjectControllerDeleteShouldCallDeleteProjectServiceAsExpected()
        {
            var request = 12345;
            subject = new ProjectController(this.mockService.Object);

            subject.Delete(request);

            this.mockService.Verify(x => x.DeleteProject(request), Times.Once);
        }

        private static ProjectModel GetProjectModel()
        {
            return new ProjectModel()
            {
                endDate = new DateTime(2018, 01, 01),
                startDate = new DateTime(2018, 01, 01),
                id = 12345,
                name = "Test",
                priority = 22,
            };
        }
    }
}
