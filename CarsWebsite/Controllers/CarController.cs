using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http.Headers;
using API.Dtos;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : Controller
    {
        private ICarService _carService;
        private IMapper _mapper;

        public CarController(ICarService carService, IMapper mapper)
        {
            this._carService = carService;
            this._mapper = mapper;
        }

        [HttpPost("newCar")]
        public IActionResult AddCar([FromBody] NewCarDto model)
        {
            var car = this._mapper.Map<Car>(model);
            car.ImagePath = "Resources\\Images\\default.jpg";

            try
            {
                this._carService.Add(car);
                return this.Ok();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpGet("allCars")]
        public IActionResult GetAll()
        {
            var cars = this._carService.GetAll();
            List<CarDto> carsDtos = new List<CarDto>();
            foreach (var car in cars)
            {
                var carDto = this._mapper.Map<CarDto>(car);
                carsDtos.Add(carDto);
            }

            return this.Ok(carsDtos);
        }

        [HttpGet("car/{id}")]
        public IActionResult GetCar(int id)
        {
            var car = this._carService.Find(id);
            var carDto = this._mapper.Map<CarDto>(car);
            return this.Ok(carDto);
        }

        [HttpPut("updateCar")]
        public IActionResult UpdateCar([FromBody] CarDto carDto)
        {
            var car = this._mapper.Map<Car>(carDto);
            car = this._carService.Update(car);
            carDto = this._mapper.Map<CarDto>(car);
            return this.Ok(carDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = this._carService.Find(id);
            if (System.IO.File.Exists(car.ImagePath) && !car.ImagePath.Contains("default", StringComparison.OrdinalIgnoreCase))
            {
                System.IO.File.Delete(car.ImagePath);
            }

            this._carService.Delete(id);
            return this.Ok();
        }

        [HttpPost("upload/{carId}"), DisableRequestSizeLimit]
        public IActionResult Upload(int carId)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileOriginalName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var extension = Path.GetExtension(fileOriginalName);
                    if (extension.ToUpper() != ".PNG" && extension.ToUpper(new CultureInfo("en-US")) != ".JPG" && extension.ToUpper(new CultureInfo("en-US")) != ".JPEG")
                    {
                        return this.BadRequest();
                    }

                    string fileNewName = carId.ToString(new CultureInfo("en-US")) + extension;
                    var fullPath = Path.Combine(pathToSave, fileNewName);
                    var dbPath = Path.Combine(folderName, fileNewName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var car = this._carService.Find(carId);
                    car.ImagePath = dbPath;
                    this._carService.Update(car);

                    return this.Ok(new { dbPath });
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
    }
}