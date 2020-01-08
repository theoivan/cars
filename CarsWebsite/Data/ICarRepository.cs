using System.Collections.Generic;
using API.Models;

namespace API.Data
{
    public interface ICarRepository
    {
        Car Find(int id);

        List<Car> GetAll();

        List<Car> GetAllForUserId(int userId);

        Car Add(Car car);

        Car Update(Car car);

        void Remove(int id);
    }
}
