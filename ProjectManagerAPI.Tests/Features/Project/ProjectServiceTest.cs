using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using ProjectManagerAPI.Features.Project;
using ProjectManagerAPI.Infrastructure;
using ProjectManagerAPI.Infrastructure.Entities;
using Assert = NUnit.Framework.Assert;

namespace ProjectManagerAPI.Tests.Features.Project
{
    [TestFixture]
    public class ProjectServiceTest
    {
        private Mock<IProjectManagerDBContext> taskContext;
        private ProjectService subject;
        private Mock<DbSet<ProjectEntity>> dbSet;

        [SetUp]
        public void ProjectServiceTestSetup()
        {
            this.taskContext = new Mock<IProjectManagerDBContext>();
            this.taskContext.SetupProperty(x => x.Projects);
            this.taskContext.Object.Projects = (GetQueryableMockDbSet(GetProjects()));
            this.subject = new ProjectService(this.taskContext.Object);
        }

        [Test]
        public void GetProjectsShouldReturnProjectsAsExpected()
        {
            var actuals = this.subject.GetProjects();

            var expected = GetProjects()[0];
            var actual = actuals[0];
            Assert.AreEqual(expected.Id, actual.id);
            Assert.AreEqual(expected.Name, actual.name);
            Assert.AreEqual(expected.Priority, actual.priority);
            Assert.AreEqual(expected.StartDate, actual.startDate);
            Assert.AreEqual(expected.EndDate, actual.endDate);
            Assert.AreEqual(expected.UserId, actual.userId);
        }

        [Test]
        public void GetProjectByIdShouldReturnProjectAsExpected()
        {
            var expected = GetProjects().First(x => x.Id == 123);

            var actual = this.subject.GetProjectById(123);

            Assert.AreEqual(expected.Id, actual.id);
            Assert.AreEqual(expected.Name, actual.name);
            Assert.AreEqual(expected.Priority, actual.priority);
            Assert.AreEqual(expected.StartDate, actual.startDate);
            Assert.AreEqual(expected.EndDate, actual.endDate);
            Assert.AreEqual(expected.UserId, actual.userId);
        }

        [Test]
        public void CreateProjectsShouldAddProjectsAsExpected()
        {
            var request = new ProjectModel()
            {
                endDate = DateTime.Now,
                startDate = DateTime.Now,
                name = "tt",
                priority = 234
            };

            this.subject.CreateProject(request);

            int actual = this.taskContext.Object.Projects.Count();
            Assert.AreEqual(3, actual);
            this.taskContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void UpdateProjectsShouldUpdateProjectsAsExpected()
        {
            var request = new ProjectModel()
            {
                endDate = DateTime.Now,
                startDate = DateTime.Now,
                name = "tt",
                priority = 123
            };

            this.subject.UpdateProject(123, request);

            var actual = this.taskContext.Object.Projects.First(x => x.Id == 123);
            Assert.AreEqual(request.name, actual.Name);
            this.taskContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void DeleteProjectsShouldRemoveProjectsAsExpected()
        {
            this.subject.DeleteProject(123);

            var actual = GetProjects().First(x => x.Id == 123);
            this.dbSet.Verify(x => x.Remove(It.IsAny<ProjectEntity>()), Times.Once);
        }

        private DbSet<ProjectEntity> GetQueryableMockDbSet(List<ProjectEntity> sourceList)
        {
            var queryable = sourceList.AsQueryable();

            this.dbSet = new Mock<DbSet<ProjectEntity>>();
            this.dbSet.As<IQueryable<ProjectEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
            this.dbSet.As<IQueryable<ProjectEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
            this.dbSet.As<IQueryable<ProjectEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            this.dbSet.As<IQueryable<ProjectEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            this.dbSet.Setup(d => d.Add(It.IsAny<ProjectEntity>())).Callback<ProjectEntity>((s) => sourceList.Add(s));
            this.dbSet.Setup(d => d.Remove(It.IsAny<ProjectEntity>())).Callback<ProjectEntity>((s) => sourceList.Remove(s));

            return dbSet.Object;
        }

        private static List<ProjectEntity> GetProjects()
        {
            return new List<ProjectEntity>()
            {
                new ProjectEntity
                {
                    EndDate = new System.DateTime(2012,12, 12),
                    StartDate = new System.DateTime(2012,12, 22),
                    Id = 12345,
                    Name = "VV Project",
                    UserId = 1234,
                    Priority = 22
                },
                new ProjectEntity
                {
                    EndDate = new System.DateTime(2000,12,12),
                    StartDate = new System.DateTime(2000,12,12),
                    Id = 123,
                    UserId = 551234,
                    Name = "VR test",
                    Priority = 2,
                }
            };
        }
    }
}
