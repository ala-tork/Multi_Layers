using AutoMapper;
using Multi_Layer.Application.DTOs;
using Multi_Layer.Domain.Entities;


namespace Multi_Layer.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
