using MediatR;
using Application.DTOs;

namespace Application.Commands
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public CreateUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
