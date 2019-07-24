using Moq;
using NUnit.Framework;
using ProjectManagerAPI.Features.User;

namespace ProjectManagerAPI.Tests.Features.User
{
    [TestFixture]
    public class UserControllerTest
    {
        private UserController subject;
        private Mock<IUserService> mockService;

        public UserControllerTest() => this.mockService = new Mock<IUserService>();

        [Test]
        public void UserControllerGetShouldCallServiceGetAsExpected()
        {
            subject = new UserController(this.mockService.Object);

            subject.Get();

            this.mockService.Verify(x => x.GetUsers(), Times.Once);
        }

        [Test]
        public void UserControllerGetByIdhouldCallServiceGetByIdAsExpected()
        {
            var request = 123;
            subject = new UserController(this.mockService.Object);

            subject.Get(request);

            this.mockService.Verify(x => x.GetUserById(request), Times.Once);
        }

        [Test]
        public void UserControllerPostShouldCallCreateUserServiceAsExpected()
        {
            var request = GetUserModel();
            subject = new UserController(this.mockService.Object);

            subject.Post(request);

            this.mockService.Verify(x => x.CreateUser(request), Times.Once);
        }

        [Test]
        public void UserControllerPutShouldCallUpdateUserServiceAsExpected()
        {
            var request = GetUserModel();
            subject = new UserController(this.mockService.Object);

            subject.Put(request.id, request);

            this.mockService.Verify(x => x.UpdateUser(request.id, request), Times.Once);
        }

        [Test]
        public void UserControllerDeleteShouldCallDeleteUserServiceAsExpected()
        {
            var request = 12345;
            subject = new UserController(this.mockService.Object);

            subject.Delete(request);

            this.mockService.Verify(x => x.DeleteUser(request), Times.Once);
        }

        private static UserModel GetUserModel()
        {
            return new UserModel()
            {
                firstName = "Test FName",
                lastName = "Test LName",
                employeeId = 12345
            };
        }
    }
}
