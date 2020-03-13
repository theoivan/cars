namespace API.Services
{
    using System;
    using API.Data;

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public void AssignRoleToUser(int userId, int roleId)
        {
            this.roleRepository.AssignRoleToUser(userId, roleId);
        }

        public string GetRoleForUserByUserId(int id)
        {
            return this.roleRepository.GetRoleForUserByUserId(id);
        }

        public int GetRoleIdByRoleName(string roleName)
        {
            return this.roleRepository.GetRoleIdByRoleName(roleName);
        }
    }
}
