
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

        public async Task<User> GetById(int userId)
        {
            return await userRepository.GetUserById(userId);
        }

   
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await userRepository.GetAllUsers();
        }

        public async Task<User> Register(RegisterRequest registerRequest)
        {
            return await userRepository.CreateUser(registerRequest.Email);
          
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await userRepository.GetUserByEmail(email);
        }

        public async Task<User> GetOrCreateUser(string email)
        {
            User user = await userRepository.GetUserByEmail(email);
            if(user != null)
            {
                return user;
            }
            return await userRepository.CreateUser(email);
        }
    }
}
