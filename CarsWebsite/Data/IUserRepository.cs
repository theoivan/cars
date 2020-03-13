namespace API.Data
{
    using API.Models;

    public interface IUserRepository
    {
        User FindByUsername(string username);

        User FindById(int id);

        User Add(User user);
    }
}
