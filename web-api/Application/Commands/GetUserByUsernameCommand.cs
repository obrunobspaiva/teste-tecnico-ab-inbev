using Application.DTOs;
using MediatR;

public class GetUserByUsernameCommand : IRequest<UserDto>
{
    public string Username { get; set; }

    public GetUserByUsernameCommand(string username)
    {
        Username = username;
    }
}
