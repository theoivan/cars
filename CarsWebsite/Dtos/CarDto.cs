namespace API.Dtos
{
    public class CarDto
    {
        public int CarId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string RegistrationNumber { get; set; }

        public int UserId { get; set; }

        public string Combustible { get; set; }

        public string FirstRegistrationDate { get; set; }

        public int EngineSize { get; set; }

        public string Transmission { get; set; }

        public string OriginCountry { get; set; }

        public int NumberOfDoors { get; set; }

        public int NumberOfSeats { get; set; }

        public string EmissionStandard { get; set; }

        public string Colour { get; set; }

        public string BodyType { get; set; }

        public int Power { get; set; }

        public string ImagePath { get; set; }
    }
}
