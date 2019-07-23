using ProjectManagerAPI.Infrastructure;
using ProjectManagerAPI.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagerAPI.Features.User
{
    public class UserService : IUserService
    {
        private readonly IProjectManagerDBContext dbContext;

        public UserService(IProjectManagerDBContext projectManagerDBContext)
        {
            this.dbContext = projectManagerDBContext;
        }

        public void CreateUser(UserModel user)
        {
            var userEntity = new UserEntity()
            {
                EmployeeId = user.employeeId,
                FirstName = user.firstName,
                LastName = user.lastName
            };
            this.dbContext.Users.Add(userEntity);
            this.dbContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            this.dbContext.Users.Remove(this.dbContext.Users.FirstOrDefault(x => x.Id == id));
            this.dbContext.SaveChanges();
        }

        public UserModel GetUserById(int id)
        {
            var user = this.dbContext.Users.FirstOrDefault(x => x.Id == id);
            
            return Map(user);
        }

        public IList<UserModel> GetUsers()
        {
            return this.dbContext.Users.ToList().Select(x => Map(x)).ToList();
        }

        public void UpdateUser(int id, UserModel user)
        {
            var userEntity = this.dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (userEntity != null)
            {
                userEntity.FirstName = user.firstName;
                userEntity.LastName = user.lastName;
                userEntity.EmployeeId = user.employeeId;
            }

            this.dbContext.SaveChanges();
        }

        private static UserModel Map(UserEntity user)
        {
            return new UserModel()
            {
                employeeId = user.EmployeeId,
                firstName = user.FirstName,
                id = user.Id,
                lastName = user.LastName,
            };
        }
    }
}