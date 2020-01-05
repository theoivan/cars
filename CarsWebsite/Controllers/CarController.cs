using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Dtos;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
            _carService = carService;
            _mapper = mapper;
        }

        [HttpPost("newCar")]
        public IActionResult AddCar([FromBody] NewCarDto model)
        {
            var car = _mapper.Map<Car>(model);
            car.ImagePath = "Resources\\Images\\default.jpg";

            try
            {
                _carService.Add(car);
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpGet("allCars")]
        public IActionResult GetAll()
        {
            var cars = _carService.GetAll();
            List<CarDto> carsDtos = new List<CarDto>();
            foreach (var car in cars)
            {
                var carDto = _mapper.Map<CarDto>(car);
                //carDto.Image = _imageService.GetDefaultImage(car.CarId).Content;
                carsDtos.Add(carDto);
            }

            return Ok(carsDtos);
        }

        [HttpGet("car/{id}")]
        public IActionResult GetCar(int id)
        {
            var car = _carService.Find(id);
            var carDto = _mapper.Map<CarDto>(car);
            return Ok(carDto);
        }

        [HttpPut("updateCar")]
        public IActionResult UpdateCar([FromBody] CarDto carDto)
        {
            var car = _mapper.Map<Car>(carDto);
            car = _carService.Update(car);
            carDto = _mapper.Map<CarDto>(car);
            return Ok(carDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            _carService.Delete(id);
            return Ok();
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
                    if (extension.ToUpper() != ".PNG" && extension.ToUpper() != ".JPG" && extension.ToUpper() != ".JPEG")
                        return BadRequest();
                    string fileNewName = carId.ToString(new CultureInfo("en-US")) + extension;
                    var fullPath = Path.Combine(pathToSave, fileNewName);
                    var dbPath = Path.Combine(folderName, fileNewName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var car = _carService.Find(carId);
                    car.ImagePath = dbPath;
                    _carService.Update(car);

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}