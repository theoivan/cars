namespace API.Data
{
    public interface IRoleRepository
    {
        int GetRoleIdByRoleName(string roleName);

        string GetRoleForUserByUserId(int id);

        void AssignRoleToUser(int userId, int roleId);
    }
}
