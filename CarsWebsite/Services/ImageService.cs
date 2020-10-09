namespace API.Services
{
    using System.Collections.Generic;
    using API.Data;
    using API.Models;

    public class ImageService : IImageService
    {
        private readonly IImageRepository imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public Image Add(Image image)
        {
            this.imageRepository.Add(image);
            return image;
        }

        public List<string> GetAllForCarId(int carId)
        {
            return this.imageRepository.GetAllForCarId(carId);
        }
    }
}
