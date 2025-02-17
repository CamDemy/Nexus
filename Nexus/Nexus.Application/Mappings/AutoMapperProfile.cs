using AutoMapper;
using Nexus.Application.Dtos;
using Nexus.Domain.Entities;

namespace Nexus.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }

}
