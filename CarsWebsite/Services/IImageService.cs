namespace API.Services
{
    using System.Collections.Generic;
    using API.Models;

    public interface IImageService
    {
        Image Add(Image image);

        List<string> GetAllForCarId(int carId);
    }
}
