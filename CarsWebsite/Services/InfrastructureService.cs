namespace API.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using API.Data;

    public enum Transmission
    {
        Automatic,
        Manual,
    }

    public class InfrastructureService : IInfrastructureService
    {
        private readonly IInfrastructureRepository infrastructureRepository;

        public InfrastructureService(IInfrastructureRepository infrastructureRepository)
        {
            this.infrastructureRepository = infrastructureRepository;
        }

        public List<string> GetAllCountries()
        {
            return this.infrastructureRepository.GetAllCountries();
        }

        public List<string> GetAllEmissionStandards()
        {
            return this.infrastructureRepository.GetAllEmissionStandards();
        }

        public List<string> GetAllFuelTypes()
        {
            return this.infrastructureRepository.GetAllFuelTypes();
        }

        public List<string> GetAllTransmissionTypes()
        {
            return Enum.GetValues(typeof(Transmission)).Cast<Transmission>().Select(v => v.ToString()).ToList();
        }
    }
}
