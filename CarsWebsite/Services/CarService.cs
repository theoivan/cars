using API.Data;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace API.Services
{
    public class CarService: ICarService
    {
        private ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public Car Add(Car car)
        {
            _carRepository.Add(car);
            return car;
        }

        public void Delete(int id)
        {
            _carRepository.Remove(id);
        }

        public Car Find(int id)
        {
            return _carRepository.Find(id);
        }

        public List<Car> GetAll()
        {
            return _carRepository.GetAll();
        }

        public List<Car> GetAllForUser(int userId)
        {
            return _carRepository.GetAllForUserId(userId);
        }

        public Car Update(Car car)
        {
            return _carRepository.Update(car);
        }
    }
}
