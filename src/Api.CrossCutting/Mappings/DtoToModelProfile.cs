using Api.Domain.Dtos.User;
using AutoMapper;
using Domain.Models;

namespace crosscutting.Mappings;

public class DtoToModelProfile : Profile
{
    public DtoToModelProfile()
    {
        CreateMap<UserModel, UserDto>()
            .ReverseMap();
        CreateMap<UserModel, UserDtoCreate>()
            .ReverseMap();
        CreateMap<UserModel, UserDtoUpdate>()
            .ReverseMap();
    }
}