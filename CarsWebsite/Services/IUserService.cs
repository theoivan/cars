using System;
using API.Models;

namespace API.Services
{
    public interface IUserService
    {
        User Register(User user, string password);

        User Login(string username, string password);

        User GetById(int id);
    }
}
