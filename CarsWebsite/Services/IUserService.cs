using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IUserService
    {
        User Register(User user, string password);
        User Login(string username, string password);
        User GetById(int id);
    }
}
