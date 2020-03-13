namespace API.Services
{
    using System.Collections.Generic;
    using API.Data;
    using API.Models;

    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;

        public CarService(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        public Car Add(Car car)
        {
            this.carRepository.Add(car);
            return car;
        }

        public void Delete(int id)
        {
            this.carRepository.Remove(id);
        }

        public Car Find(int id)
        {
            return this.carRepository.Find(id);
        }

        public List<Car> GetAll()
        {
            return this.carRepository.GetAll();
        }

        public List<Car> GetAllForUser(int userId)
        {
            return this.carRepository.GetAllForUserId(userId);
        }

        public Car Update(Car car)
        {
            return this.carRepository.Update(car);
        }
    }
}
