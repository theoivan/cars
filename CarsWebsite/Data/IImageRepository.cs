namespace API.Data
{
    using System.Collections.Generic;
    using API.Models;

    public interface IImageRepository
    {
        Image Add(Image image);

        List<string> GetAllForCarId(int carId);
    }
}
