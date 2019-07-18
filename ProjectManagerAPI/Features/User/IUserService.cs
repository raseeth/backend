using System.Collections.Generic;

namespace ProjectManagerAPI.Features.User
{
    public interface IUserService
    {
        void CreateUser(UserModel user);

        void DeleteUser(int id);

        UserModel GetUserById(int id);

        IList<UserModel> GetUsers();

        void UpdateUser(int id, UserModel user);
    }
}
