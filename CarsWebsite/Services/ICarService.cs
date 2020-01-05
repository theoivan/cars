using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ICarService
    {
        Car Add(Car car);
        List<Car> GetAll();
        List<Car> GetAllForUser(int userId);
        Car Find(int id);
        Car Update(Car car);
        void Delete(int id);
    }
}
