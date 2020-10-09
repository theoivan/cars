namespace API.Data
{
    using System.Collections.Generic;

    public interface IInfrastructureRepository
    {
        List<string> GetAllCountries();

        List<string> GetAllBodyTypes();

        List<string> GetAllFuelTypes();

        List<string> GetAllEmissionStandards();
    }
}
