using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using UserService.Domain;

namespace UserService.Infrastructure
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
    }

    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<User> CreateAsync(User user)
        {
            const string sql = @"
                INSERT INTO Users (Id, Email, FullName)
                VALUES (@Id, @Email, @FullName);";

            user.Id = user.Id == Guid.Empty ? Guid.NewGuid() : user.Id;

            using var connection = CreateConnection();
            await connection.ExecuteAsync(sql, new
            {
                user.Id,
                user.Email,
                user.FullName
            });

            return user;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            const string sql = @"
                    SELECT Id, Email, FullName
                    FROM Users
                    WHERE Id = @Id;";

            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            const string sql = @"
            SELECT Id, Email, FullName
            FROM Users;";

            using var connection = CreateConnection();
            return await connection.QueryAsync<User>(sql);
        }
    }
}