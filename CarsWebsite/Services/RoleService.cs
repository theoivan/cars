using API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class RoleService: IRoleService
    {
        private IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public void AssignRoleToUser(int userId, int roleId)
        {
            this._roleRepository.AssignRoleToUser(userId, roleId);
        }

        public string GetRoleForUserByUserId(int id)
        {
            return this._roleRepository.GetRoleForUserByUserId(id);
        }

        public int GetRoleIdByRoleName(string roleName)
        {
            return this._roleRepository.GetRoleIdByRoleName(roleName);
        }
    }
}
