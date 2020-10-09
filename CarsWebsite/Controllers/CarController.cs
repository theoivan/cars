namespace API.Controllers
{
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

    [ApiController]
    [Route("[controller]")]
    public class CarController : Controller
    {
        private readonly ICarService carService;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public CarController(ICarService carService, IImageService imageService, IMapper mapper)
        {
            this.carService = carService;
            this.imageService = imageService;
            this.mapper = mapper;
        }

        [HttpPost("newCar")]
        public IActionResult AddCar([FromBody] NewCarDto model)
        {
            var car = this.mapper.Map<Car>(model);
            try
            {
                this.carService.Add(car);
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
            var cars = this.carService.GetAll();
            List<CarDto> carsDtos = new List<CarDto>();
            foreach (var car in cars)
            {
                var carDto = this.mapper.Map<CarDto>(car);
                carsDtos.Add(carDto);
            }

            return this.Ok(carsDtos);
        }

        [HttpGet("car/{id}")]
        public IActionResult GetCar(int id)
        {
            var car = this.carService.Find(id);

            // car.ImagesPath = this.imageService.GetAllForCarId(id);
            var carDto = this.mapper.Map<CarDto>(car);
            /*if (carDto.ImagesPath.Count == 0)
            {
                carDto.ImagesPath.Add(this.defaultImagePath);
            }*/

            return this.Ok(carDto);
        }

        [HttpPut("updateCar")]
        public IActionResult UpdateCar([FromBody] CarDto carDto)
        {
            var car = this.mapper.Map<Car>(carDto);
            car = this.carService.Update(car);
            carDto = this.mapper.Map<CarDto>(car);
            return this.Ok(carDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = this.carService.Find(id);
            /* if (System.IO.File.Exists(car.ImagePath) && !car.ImagePath.Contains("default", StringComparison.OrdinalIgnoreCase))
            {
                System.IO.File.Delete(car.ImagePath);
            } */

            this.carService.Delete(id);
            return this.Ok();
        }
    }
}