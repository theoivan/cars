using API.Dtos;
using API.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<Car, CarDto>().ForMember(x => x.FirstRegistrationDate, opt => opt.MapFrom(x => x.FirstRegistrationDate.ToShortDateString()));
            CreateMap<CarDto, Car>().ForMember(x => x.FirstRegistrationDate, opt => opt.MapFrom(x => DateTime.Parse(x.FirstRegistrationDate)));
            CreateMap<NewCarDto, Car>().ForMember(x => x.FirstRegistrationDate, opt => opt.MapFrom(x => DateTime.Parse(x.FirstRegistrationDate)));
        }
    }
}
