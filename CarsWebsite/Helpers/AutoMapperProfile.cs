namespace API.Helpers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using API.Dtos;
    using API.Models;
    using AutoMapper;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<RegisterDto, User>();
            this.CreateMap<Car, CarDto>().ForMember(x => x.FirstRegistrationDate, opt => opt.MapFrom(x => x.FirstRegistrationDate.ToShortDateString()))
                                        .ForMember(x => x.ImagesPath, opt => opt.MapFrom(x => x.ImagesPath.Select(x => x.ImagePath)));
            this.CreateMap<CarDto, Car>().ForMember(x => x.FirstRegistrationDate, opt => opt.MapFrom(x => DateTime.Parse(x.FirstRegistrationDate, new CultureInfo("en-US"))));
            this.CreateMap<NewCarDto, Car>().ForMember(x => x.FirstRegistrationDate, opt => opt.MapFrom(x => DateTime.Parse(x.FirstRegistrationDate, new CultureInfo("en-US"))));
        }
    }
}
