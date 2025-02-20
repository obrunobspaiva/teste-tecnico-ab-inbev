using Application.Commands;
using Application.DTOs;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserCommand, User>();
    }
}
