using API.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class CarRepository : ICarRepository
    {
        private IDbConnection db;

        public CarRepository(string connStrings)
        {
            this.db = new SqlConnection(connStrings);
        }

        public Car Add(Car car)
        {
            var sql = "INSERT INTO Cars (Brand, Model, RegistrationNumber, UserId, Combustible, FirstRegistrationDate, EngineSize, Transmission, OriginCountry, " +
                "NumberOfDoors, NumberOfSeats, EmissionStandard, Colour, BodyType, Power, ImagePath) VALUES (@Brand, @Model, @RegistrationNumber, @UserId, @Combustible, " +
                "@FirstRegistrationDate, @EngineSize, @Transmission, @OriginCountry, @NumberOfDoors, @NumberOfSeats, @EmissionStandard, @Colour, @BodyType, @Power, " +
                "@ImagePath); SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = this.db.QueryFirstOrDefaultAsync<int>(sql, car).Result;
            car.CarId = id;
            return car;
        }

        public Car Find(int id)
        {
            return this.db.QueryFirstOrDefaultAsync<Car>("SELECT TOP 1 * FROM Cars WHERE CarId = @Id", new { id }).Result;
        }

        public List<Car> GetAll()
        {
            return this.db.QueryAsync<Car>("SELECT * FROM Cars").Result.ToList();
        }

        public List<Car> GetAllForUserId(int userId)
        {
            return this.db.QueryAsync<Car>("SELECT * FROM Cars WHERE UserId = @UserId", new { userId }).Result.ToList();
        }

        public void Remove(int carId)
        {
            this.db.ExecuteAsync("DELETE FROM Cars WHERE CarId = @CarId", new { carId });
        }

        public Car Update(Car car)
        {
            var sql = "UPDATE Cars SET " +
                "Brand = @Brand, Model = @Model, RegistrationNumber = @RegistrationNumber, Combustible = @Combustible, FirstRegistrationDate = @FirstRegistrationDate, " +
                "EngineSize = @EngineSize, Transmission = @Transmission, OriginCountry = @OriginCountry, NumberOfDoors = @NumberOfDoors, NumberOfSeats = @NumberOfSeats, " +
                "EmissionStandard = @EmissionStandard, Colour = @Colour, BodyType = @BodyType, Power = @Power, ImagePath = @ImagePath WHERE CarId = @CarId";
            this.db.ExecuteAsync(sql, car);
            return car;
        }
    }
}
