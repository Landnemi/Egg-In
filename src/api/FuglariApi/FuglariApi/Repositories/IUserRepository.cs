using FuglariApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> CreateUser(string email);
    }
}
