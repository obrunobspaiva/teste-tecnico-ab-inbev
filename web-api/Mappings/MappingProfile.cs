using Application.Commands;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserCommand, User>();
    }
}
