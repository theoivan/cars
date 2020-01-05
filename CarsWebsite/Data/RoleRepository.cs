using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class RoleRepository: IRoleRepository
    {
        private IDbConnection db;

        public RoleRepository(string connStrings)
        {
            this.db = new SqlConnection(connStrings);
        }

        public int GetRoleIdByRoleName(string roleName)
        {
            return this.db.QueryFirstOrDefaultAsync<int>("SELECT TOP 1 RoleId FROM Roles WHERE RoleName=@RoleName", new { roleName }).Result;
        }

        public string GetRoleForUserByUserId(int id)
        {
            var roleId = this.db.QueryFirstOrDefaultAsync<int>("SELECT RoleId FROM UsersRoles WHERE UserId = @Id", new { id }).Result;
            var roleName = this.db.QueryFirstOrDefaultAsync<string>("SELECT RoleName FROM Roles WHERE RoleId = @RoleId", new { roleId }).Result;
            return roleName;
        }

        public void AssignRoleToUser(int userId, int roleId)
        {
            this.db.ExecuteAsync("INSERT INTO UsersRoles (UserId, RoleId) VALUES (@UserId, @RoleId)", new { userId, roleId });
        }
    }
}
