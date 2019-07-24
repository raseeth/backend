using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using ProjectManagerAPI.Features.User;
using ProjectManagerAPI.Infrastructure;
using ProjectManagerAPI.Infrastructure.Entities;
using Assert = NUnit.Framework.Assert;

namespace ProjectManagerAPI.Tests.Features.User
{
    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IProjectManagerDBContext> taskContext;
        private UserService subject;
        private Mock<DbSet<UserEntity>> dbSet;

        [SetUp]
        public void UserServiceTestSetup()
        {
            this.taskContext = new Mock<IProjectManagerDBContext>();
            this.taskContext.SetupProperty(x => x.Users);
            this.taskContext.Object.Users = (GetQueryableMockDbSet(GetUsers()));
            this.subject = new UserService(this.taskContext.Object);
        }

        [Test]
        public void GetUsersShouldReturnUsersAsExpected()
        {
            var actuals = this.subject.GetUsers();

            var expected = GetUsers()[0];
            var actual = actuals[0];
            Assert.AreEqual(expected.Id, actual.id);
            Assert.AreEqual(expected.FirstName, actual.firstName);
            Assert.AreEqual(expected.LastName, actual.lastName);
            Assert.AreEqual(expected.EmployeeId, actual.employeeId);
        }

        [Test]
        public void GetUserByIdShouldReturnUserAsExpected()
        {
            var expected = GetUsers().First(x => x.Id == 123);

            var actual = this.subject.GetUserById(123);

            Assert.AreEqual(expected.Id, actual.id);
            Assert.AreEqual(expected.FirstName, actual.firstName);
            Assert.AreEqual(expected.LastName, actual.lastName);
            Assert.AreEqual(expected.EmployeeId, actual.employeeId);
        }

        [Test]
        public void CreateUsersShouldAddUsersAsExpected()
        {
            var request = new UserModel()
            {
                firstName = "Test FName",
                lastName = "Test LName",
                employeeId = 12345
            };

            this.subject.CreateUser(request);

            int actual = this.taskContext.Object.Users.Count();
            Assert.AreEqual(3, actual);
            this.taskContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void UpdateUsersShouldUpdateUsersAsExpected()
        {
            var request = new UserModel()
            {
                firstName = "Test update FName",
                lastName = "Test update LName",
                employeeId = 12343
            };

            this.subject.UpdateUser(123, request);

            var actual = this.taskContext.Object.Users.First(x => x.Id == 123);
            Assert.AreEqual(request.firstName, actual.FirstName);
            Assert.AreEqual(request.lastName, actual.LastName);
            Assert.AreEqual(request.employeeId, actual.EmployeeId);
            this.taskContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void DeleteUsersShouldRemoveUsersAsExpected()
        {
            this.subject.DeleteUser(123);

            var actual = GetUsers().First(x => x.Id == 123);
            this.dbSet.Verify(x => x.Remove(It.IsAny<UserEntity>()), Times.Once);
        }

        private DbSet<UserEntity> GetQueryableMockDbSet(List<UserEntity> sourceList)
        {
            var queryable = sourceList.AsQueryable();

            this.dbSet = new Mock<DbSet<UserEntity>>();
            this.dbSet.As<IQueryable<UserEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
            this.dbSet.As<IQueryable<UserEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
            this.dbSet.As<IQueryable<UserEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            this.dbSet.As<IQueryable<UserEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            this.dbSet.Setup(d => d.Add(It.IsAny<UserEntity>())).Callback<UserEntity>((s) => sourceList.Add(s));
            this.dbSet.Setup(d => d.Remove(It.IsAny<UserEntity>())).Callback<UserEntity>((s) => sourceList.Remove(s));

            return dbSet.Object;
        }

        private static List<UserEntity> GetUsers()
        {
            return new List<UserEntity>()
            {
                new UserEntity
                {
                    Id = 12345,
                    FirstName = "Test FName",
                    LastName = "Test LName",
                    EmployeeId = 12345
                },
                new UserEntity
                {
                    Id = 123,
                    FirstName = "Test FName",
                    LastName = "Test LName",
                    EmployeeId = 12345
                }
            };
        }
    }
}
