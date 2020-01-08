using System.Collections.Generic;
using API.Data;
using API.Models;

namespace API.Services
{
    public class CarService : ICarService
    {
        private ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            this._carRepository = carRepository;
        }

        public Car Add(Car car)
        {
            this._carRepository.Add(car);
            return car;
        }

        public void Delete(int id)
        {
            this._carRepository.Remove(id);
        }

        public Car Find(int id)
        {
            return this._carRepository.Find(id);
        }

        public List<Car> GetAll()
        {
            return this._carRepository.GetAll();
        }

        public List<Car> GetAllForUser(int userId)
        {
            return this._carRepository.GetAllForUserId(userId);
        }

        public Car Update(Car car)
        {
            return this._carRepository.Update(car);
        }
    }
}
