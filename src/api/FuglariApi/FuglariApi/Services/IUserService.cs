using FuglariApi.Models;
using FuglariApi.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.Services
{
    public interface IUserService
    {

        Task<User> GetById(int userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> Register(RegisterRequest request);
    }
}
