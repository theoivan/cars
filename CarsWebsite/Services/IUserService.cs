namespace API.Services
{
    using API.Models;

    public interface IUserService
    {
        User Register(User user, string password);

        User Login(string username, string password);

        User GetById(int id);
    }
}
