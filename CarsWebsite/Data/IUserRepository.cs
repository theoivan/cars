using System;
using API.Models;

namespace API.Data
{
    public interface IUserRepository
    {
        User FindByUsername(string username);

        User FindById(int id);

        User Add(User user);
    }
}
