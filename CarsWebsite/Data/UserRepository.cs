namespace API.Data
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using API.Models;
    using Dapper;

    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection db;

        public UserRepository(string connStrings)
        {
            this.db = new SqlConnection(connStrings);
        }

        public User Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var sql = "INSERT INTO Users (Username, Email, PasswordHash, Salt)" +
                "VALUES (@Username, @Email, @PasswordHash, @Salt); " +
                "SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = this.db.QueryFirstOrDefaultAsync<int>(sql, user).Result;
            user.UserId = id;
            return user;
        }

        public User FindByUsername(string username)
        {
            return this.db.QueryFirstOrDefaultAsync<User>("SELECT TOP 1 * FROM Users WHERE Username=@Username", new { username }).Result;
        }

        public User FindById(int id)
        {
            return this.db.QueryFirstOrDefaultAsync<User>("SELECT TOP 1 * FROM Users WHERE UserId=@UserId", new { id }).Result;
        }
    }
}
