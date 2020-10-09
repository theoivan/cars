namespace API.Controllers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net.Http.Headers;
    using API.Models;
    using API.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("image")]
    public class ImageController : Controller
    {
        private readonly string folderName = Path.Combine("Resources", "Images");
        private readonly string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Resources", "Images"));

        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost("upload/{carId}")]
        [DisableRequestSizeLimit]
        public IActionResult Upload(int carId)
        {
            try
            {
                var file = this.Request.Form.Files[0];

                if (file.Length > 0)
                {
                    var fileOriginalName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var extension = Path.GetExtension(fileOriginalName);
                    if (extension.ToUpper() != ".PNG" && extension.ToUpper(new CultureInfo("en-US")) != ".JPG" && extension.ToUpper(new CultureInfo("en-US")) != ".JPEG")
                    {
                        return this.BadRequest();
                    }

                    string fileNewName = Guid.NewGuid().ToString() + extension;
                    var fullPath = Path.Combine(this.pathToSave, fileNewName);
                    var dbPath = Path.Combine(this.folderName, fileNewName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var image = new Image
                    {
                        CarId = carId,
                        ImagePath = dbPath,
                    };

                    this.imageService.Add(image);

                    return this.Ok(new { image });
                }
                else
                {
                    return this.BadRequest();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpGet("getAllForCarId/{carId}")]
        public IActionResult GetCar(int carId)
        {
            var paths = this.imageService.GetAllForCarId(carId);

            return this.Ok(paths);
        }
    }
}
