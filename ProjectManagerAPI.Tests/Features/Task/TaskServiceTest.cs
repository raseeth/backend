using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using ProjectManagerAPI.Features.Task;
using ProjectManagerAPI.Infrastructure;
using ProjectManagerAPI.Infrastructure.Entities;
using Assert = NUnit.Framework.Assert;

namespace ProjectManagerAPI.Tests.Features.Task
{
    [TestFixture]
    public class TaskServiceTest
    {
        private Mock<IProjectManagerDBContext> taskContext;
        private TaskService subject;
        private Mock<DbSet<TaskEntity>> dbSet;

        [SetUp]
        public void TaskServiceTestSetup()
        {
            this.taskContext = new Mock<IProjectManagerDBContext>();
            this.taskContext.SetupProperty(x => x.Tasks);
            this.taskContext.Object.Tasks = (GetQueryableMockDbSet(GetTasks()));
            this.subject = new TaskService(this.taskContext.Object);
        }

        [Test]
        public void GetTasksShouldReturnTasksAsExpected()
        {
            var actuals = this.subject.GetTasks();

            var expected = GetTasks()[0];
            var actual = actuals[0];
            Assert.AreEqual(expected.Id, actual.id);
            Assert.AreEqual(expected.Name, actual.name);
            Assert.AreEqual(expected.ParentTaskId, actual.parentTaskId);
            Assert.AreEqual(expected.Priority, actual.priority);
            Assert.AreEqual(expected.StartDate, actual.startDate);
            Assert.AreEqual(expected.EndDate, actual.endDate);
            Assert.AreEqual(expected.IsCompleted, actual.isCompleted);
            Assert.AreEqual(expected.ParentTaskId, actual.parentTaskId);
            Assert.AreEqual(expected.UserId, actual.userId);
        }

        [Test]
        public void GetTaskByIdShouldReturnTaskAsExpected()
        {
            var expected = GetTasks().First(x => x.Id == 123);

            var actual = this.subject.GetTaskById(123);

            Assert.AreEqual(expected.Id, actual.id);
            Assert.AreEqual(expected.Name, actual.name);
            Assert.AreEqual(expected.ParentTaskId, actual.parentTaskId);
            Assert.AreEqual(expected.Priority, actual.priority);
            Assert.AreEqual(expected.StartDate, actual.startDate);
            Assert.AreEqual(expected.EndDate, actual.endDate);
            Assert.AreEqual(expected.IsCompleted, actual.isCompleted);
            Assert.AreEqual(expected.IsParentTask, actual.isParentTask);
            Assert.AreEqual(expected.ParentTaskId, actual.parentTaskId);
            Assert.AreEqual(expected.UserId, actual.userId);
        }

        [Test]
        public void CreateTasksShouldAddTasksAsExpected()
        {
            var request = new TaskModel()
            {
                endDate = DateTime.Now,
                startDate = DateTime.Now,
                name = "tt",
                priority = 234,
                isParentTask = false,
                isCompleted = false
            };

            this.subject.CreateTask(request);

            int actual = this.taskContext.Object.Tasks.Count();
            Assert.AreEqual(3, actual);
            this.taskContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void UpdateTasksShouldUpdateTasksAsExpected()
        {
            var request = new TaskModel()
            {
                endDate = DateTime.Now,
                startDate = DateTime.Now,
                name = "tt",
                isParentTask = false,
                priority = 123,
                isCompleted = false
            };

            this.subject.UpdateTask(123, request);

            var actual = this.taskContext.Object.Tasks.First(x => x.Id == 123);
            Assert.AreEqual(request.name, actual.Name);
            this.taskContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void DeleteTasksShouldRemoveTasksAsExpected()
        {
            this.subject.DeleteTask(123);

            var actual = GetTasks().First(x => x.Id == 123);
            this.dbSet.Verify(x => x.Remove(It.IsAny<TaskEntity>()), Times.Once);
        }

        private DbSet<TaskEntity> GetQueryableMockDbSet(List<TaskEntity> sourceList)
        {
            var queryable = sourceList.AsQueryable();

            this.dbSet = new Mock<DbSet<TaskEntity>>();
            this.dbSet.As<IQueryable<TaskEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
            this.dbSet.As<IQueryable<TaskEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
            this.dbSet.As<IQueryable<TaskEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            this.dbSet.As<IQueryable<TaskEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            this.dbSet.Setup(d => d.Add(It.IsAny<TaskEntity>())).Callback<TaskEntity>((s) => sourceList.Add(s));
            this.dbSet.Setup(d => d.Remove(It.IsAny<TaskEntity>())).Callback<TaskEntity>((s) => sourceList.Remove(s));

            return dbSet.Object;
        }

        private static List<TaskEntity> GetTasks()
        {
            return new List<TaskEntity>()
            {
                new TaskEntity
                {
                    EndDate = new System.DateTime(2012,12, 12),
                    StartDate = new System.DateTime(2012,12, 22),
                    Id = 12345,
                    Name = "VV Task",
                    ParentTaskId = 123,
                    UserId = 1234,
                    ProjectId = 124,
                    IsParentTask = false,
                    Priority = 22,
                    IsCompleted = true
                },
                new TaskEntity
                {
                    EndDate = new System.DateTime(2000,12,12),
                    StartDate = new System.DateTime(2000,12,12),
                    Id = 123,
                    UserId = 551234,
                    ProjectId = 76124,
                    IsParentTask = true,
                    Name = "VR test",
                    ParentTaskId = 12,
                    Priority = 2,
                    IsCompleted = true
                }
            };
        }
    }
}
