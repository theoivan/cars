namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using API.Models;
    using Dapper;

    public class ImageRepository : IImageRepository
    {
        private readonly IDbConnection db;

        public ImageRepository(string connStrings)
        {
            this.db = new SqlConnection(connStrings);
        }

        public Image Add(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            var sql = "INSERT INTO CarImages (CarId, ImagePath) VALUES (@CarId, @ImagePath); SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = this.db.QueryFirstOrDefaultAsync<int>(sql, image).Result;
            image.Id = id;
            return image;
        }

        public List<string> GetAllForCarId(int carId)
        {
            return this.db.QueryAsync<string>("SELECT ImagePath FROM CarImages WHERE CarId = @CarId", new { carId }).Result.ToList();
        }
    }
}
