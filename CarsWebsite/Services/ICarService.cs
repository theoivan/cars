namespace API.Services
{
    using System.Collections.Generic;
    using API.Models;

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
