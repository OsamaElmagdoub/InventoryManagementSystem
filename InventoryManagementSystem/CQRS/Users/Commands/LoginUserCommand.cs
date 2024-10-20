
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Enums;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Base;
using MediatR;


namespace InventoryManagementSystem.CQRS.Users.Commands
{
    public record LoginUserCommand(UserLoginDto userLoginDto) : IRequest<ResultDTO<string>>;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultDTO<string>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMediator _mediator;

        public LoginUserCommandHandler(IRepository<User> userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }
        public async Task<ResultDTO<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.First(c => c.Email == request.userLoginDto.Email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.userLoginDto.Password, user.Password))
            {
                return ResultDTO<string>.Faliure(ErrorCode.WrongPasswordOrEmail, "Email or Password is incorrect");
            }
            var userDTO = user.MapOne<UserDto>();
            var token = TokenGenerator.GenerateToken(userDTO);

            return  ResultDTO<int>.Sucess(token, "User Login Successfully!");
        }
    }
}
