using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IRoleService
    {
        string GetRoleForUserByUserId(int id);
        int GetRoleIdByRoleName(string roleName);
        void AssignRoleToUser(int userId, int roleId);
    }
}
