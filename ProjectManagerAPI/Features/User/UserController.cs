using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectManagerAPI.Features.User
{
    public class UserController : ApiController
    {
        private readonly IUserService service;

        public UserController(IUserService userService)
        {
            this.service = userService;
        }

        public IEnumerable<UserModel> Get()
        {
            return this.service.GetUsers();
        }

        public UserModel Get(int id)
        {
            return this.service.GetUserById(id);
        }

        public void Post([FromBody]UserModel value)
        {
            this.service.CreateUser(value);
        }

        public void Put(int id, [FromBody]UserModel value)
        {
            this.service.UpdateUser(id, value);
        }

        public void Delete(int id)
        {
            this.service.DeleteUser(id);
        }
    }
}
