using Application.DTOs;
using MediatR;

public class GetUserQueryHandler : IRequestHandler<GetUserByUsernameCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(GetUserByUsernameCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Username);
        
        if (user == null)
            return null;

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username
        };
    }
}
