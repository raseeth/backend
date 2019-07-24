using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using ProjectManagerAPI.Features.Task;

namespace ProjectManagerAPI.Tests.Features.Task
{
    [TestFixture]
    public class TaskControllerTest
    {
        private TaskController subject;
        private Mock<ITaskService> mockService;

        public TaskControllerTest()
        {
            this.mockService = new Mock<ITaskService>();
        }

        [Test]
        public void TaskControllerGetShouldCallServiceGetAsExpected()
        {
            subject = new TaskController(this.mockService.Object);

            subject.Get();

            this.mockService.Verify(x => x.GetTasks(), Times.Once);
        }

        [Test]
        public void TaskControllerGetByIdhouldCallServiceGetByIdAsExpected()
        {
            var request = 123;
            subject = new TaskController(this.mockService.Object);

            subject.Get(request);

            this.mockService.Verify(x => x.GetTaskById(request), Times.Once);
        }

        [Test]
        public void TaskControllerPostShouldCallCreateTaskServiceAsExpected()
        {
            var request = GetTaskModel();
            subject = new TaskController(this.mockService.Object);

            subject.Post(request);

            this.mockService.Verify(x => x.CreateTask(request), Times.Once);
        }

        [Test]
        public void TaskControllerPutShouldCallUpdateTaskServiceAsExpected()
        {
            var request = GetTaskModel();
            subject = new TaskController(this.mockService.Object);

            subject.Put(request.id, request);

            this.mockService.Verify(x => x.UpdateTask(request.id, request), Times.Once);
        }

        [Test]
        public void TaskControllerDeleteShouldCallDeleteTaskServiceAsExpected()
        {
            var request = 12345;
            subject = new TaskController(this.mockService.Object);

            subject.Delete(request);

            this.mockService.Verify(x => x.DeleteTask(request), Times.Once);
        }

        private static TaskModel GetTaskModel()
        {
            return new TaskModel()
            {
                endDate = new DateTime(2018, 01, 01),
                startDate = new DateTime(2018, 01, 01),
                id = 12345,
                name = "Test",
                parentTaskId = 12,
                priority = 22,
            };
        }
    }
}
