using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public interface IUserRepository
    {
        User FindByUsername(string username);
        User FindById(int id);
        User Add(User user);
    }
}
