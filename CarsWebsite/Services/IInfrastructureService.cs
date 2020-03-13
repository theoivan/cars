namespace API.Services
{
    using System.Collections.Generic;

    public interface IInfrastructureService
    {
        List<string> GetAllCountries();

        List<string> GetAllFuelTypes();

        List<string> GetAllEmissionStandards();

        List<string> GetAllTransmissionTypes();
    }
}
