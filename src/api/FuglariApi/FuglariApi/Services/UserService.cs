
using FuglariApi.Infrastructure;
using FuglariApi.Models;
using FuglariApi.Repositories;
using FuglariApi.RequestModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FuglariApi.Services
{
    public class UserService : IUserService
    {
        

        private readonly JwtSettings _jwtSettings;
        private readonly IUserRepository userRepository;
        public UserService(IOptions<JwtSettings> appSettings, IUserRepository repository)
        {
            _jwtSettings = appSettings.Value;
            userRepository = repository;
        }

        public Task<User> GetById(int userId)
        {
            throw new NotImplementedException();
        }

    
  
        

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await userRepository.GetAllUsers();
        }

      

        public async Task<User> Register(RegisterRequest registerRequest)
        {
            return await userRepository.CreateUser(registerRequest.Email);
          
        }
    }
}
