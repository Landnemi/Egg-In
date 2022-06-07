using FuglariApi.Infrastructure;
using FuglariApi.Models;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FuglariApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string connectionString;
        public UserRepository(IOptions<PsqlSettings> psqlSettings)
        {
            connectionString = "User ID=postgres;Host=178.128.33.244;Port=5432;Database=Fuglari;password=b1rdm4n;Pooling=true;Connection Lifetime=0;Encrypt=False;TrustServerCertificate=True"; // psqlSettings.Value.ConnectionString;
            //connectionString = "User ID=postgres;Host=localhost;Port=5432;Database=Fuglari;password=b1rdm4n;Pooling=true;Connection Lifetime=0;"; // psqlSettings.Value.ConnectionString;
        }

        private string _selectAllUsersSql = $@"select
    u.id as {nameof(User.Id)},
    u.email as {nameof(User.Email)}
from public.users u";
        public async Task<User> GetUserById(int userId)
        {
            StringBuilder queryBuilder = new StringBuilder(_selectAllUsersSql);
            queryBuilder.Append($@" where u.id = {userId}");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return await connection.QuerySingleOrDefaultAsync<User>(queryBuilder.ToString());
            }
        }
        public async Task<User> GetUserByEmail(string email)
        {
            StringBuilder queryBuilder = new StringBuilder(_selectAllUsersSql);
            queryBuilder.Append($@" where u.email = '{email}'");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return await connection.QuerySingleOrDefaultAsync<User>(queryBuilder.ToString());
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            StringBuilder queryBuilder = new StringBuilder(_selectAllUsersSql);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<User>(queryBuilder.ToString());
            }
        }

        private string _insertUserQuery = $@"INSERT INTO public.users (email) values (@email) RETURNING id";
        public async Task<User> CreateUser(string email)
        {
            StringBuilder queryBuilder = new StringBuilder(_insertUserQuery);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                    connection.Open();
                int newId = await connection.QuerySingleAsync<int>(queryBuilder.ToString(), new { email = email });
                return await connection.QuerySingleOrDefaultAsync<User>(_selectAllUsersSql + " WHERE id = " + newId);
            }
        }
    }
}
