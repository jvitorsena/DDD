using Api.Domain.Entities;
using AutoMapper;
using Domain.Models;

namespace crosscutting.Mappings;

public class ModelToEntityProfile : Profile
{
    public ModelToEntityProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .ReverseMap();
    }
}