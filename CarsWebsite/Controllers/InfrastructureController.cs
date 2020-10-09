namespace API.Controllers
{
    using System.Linq;
    using API.Dtos;
    using API.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("infrastructure")]
    public class InfrastructureController : Controller
    {
        private readonly IInfrastructureService infrastructureService;

        public InfrastructureController(IInfrastructureService infrastructureService)
        {
            this.infrastructureService = infrastructureService;
        }

        [HttpGet("allCountries")]
        public IActionResult GetAllCountries()
        {
            var countries = this.infrastructureService.GetAllCountries();

            var countriesSelectItemList = countries.Select(x => new SelectItemString()
            {
                Value = x,
                Label = x,
            }).ToList();

            return this.Ok(countriesSelectItemList);
        }

        [HttpGet("allFuelTypes")]
        public IActionResult GetAllFuelTypes()
        {
            var fuelTypes = this.infrastructureService.GetAllFuelTypes();

            var fuelTypesSelectItemList = fuelTypes.Select(x => new SelectItemString()
            {
                Value = x,
                Label = x,
            }).ToList();

            return this.Ok(fuelTypesSelectItemList);
        }

        [HttpGet("allBodyTypes")]
        public IActionResult GetAllBodyTypes()
        {
            var bodyTypes = this.infrastructureService.GetAllBodyTypes();

            var bodyTypesSelectItemList = bodyTypes.Select(x => new SelectItemString()
            {
                Value = x,
                Label = x,
            }).ToList();

            return this.Ok(bodyTypesSelectItemList);
        }

        [HttpGet("allTransmissionTypes")]
        public IActionResult GetAllTransmissionTypes()
        {
            var transmissionTypes = this.infrastructureService.GetAllTransmissionTypes();

            var transmissionTypesSelectItemList = transmissionTypes.Select(x => new SelectItemString()
            {
                Value = x,
                Label = x,
            }).ToList();

            return this.Ok(transmissionTypesSelectItemList);
        }

        [HttpGet("allEmissionStandards")]
        public IActionResult GetAllEmissionStandards()
        {
            var emissionStandards = this.infrastructureService.GetAllEmissionStandards();

            var emissionStandardsSelectItemList = emissionStandards.Select(x => new SelectItemString()
            {
                Value = x,
                Label = x,
            }).ToList();

            return this.Ok(emissionStandardsSelectItemList);
        }
    }
}