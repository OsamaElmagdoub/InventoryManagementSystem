
using InventoryManagementSystem.CQRS.Users.Queries;
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Enums;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Base;
using MediatR;

namespace InventoryManagementSystem.CQRS.Users.Commands
{
    public record RegisterUserCommand(
        string UserName, string Password, string ConfirmPassword,
        string Email, string Phone, string Country) : IRequest<ResultDTO<int>>;
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResultDTO<int>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMediator _mediator;
        public RegisterUserCommandHandler(IRepository<User> userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }
        public async Task<ResultDTO<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            //var result = await _mediator.Send(new IsEmailExistQuery(request.Email));
            //if (result.IsSuccess)
            //{
            //    return ResultDTO<int>.Faliure(ErrorCode.EmailAlreadyExist, "Email is already Exists");
            //}
            //result = await _mediator.Send(new IsUserNameExistQuery(request.UserName));
            //if (result.IsSuccess)
            //{
            //    return ResultDTO<int>.Faliure(ErrorCode.UserNameAlreadyExist, "User Name is already Exists");
            //}

            //if (request.Password != request.ConfirmPassword)
            //{
            //    return ResultDTO<int>.Faliure(ErrorCode.PasswordsDontMatch, "Passwords don't match");
            //}
            var user = request.MapOne<User>();
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var otpCode = OTPGenerator.GenerateOTP();
            user.OtpCode = otpCode;
            user.OtpExpiry = DateTime.UtcNow.AddMinutes(90);
            _userRepository.AddAsync(user);
            _userRepository.SaveChanges();
            EmailService.SendEmail(user.Email, "OTP", otpCode);
            return ResultDTO<int>.Sucess(user.Id, "User Added Successfully");
        }
    }
}
