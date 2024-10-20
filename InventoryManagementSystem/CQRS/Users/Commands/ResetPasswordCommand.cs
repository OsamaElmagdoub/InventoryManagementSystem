
using InventoryManagementSystem.CQRS.Users.Queries;
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Enums;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Base;
using MediatR;

namespace InventoryManagementSystem.CQRS.Users.Commands
{
    public record ResetPasswordCommand(string NewPassword,string ConfirmNewPassword,string Email,string OtpCode) :IRequest<ResultDTO<bool>>;
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResultDTO<bool>>
    {
        private IMediator _mediator;
        private IRepository<User> _userRepository;
        public ResetPasswordCommandHandler(IMediator mediator, IRepository<User> userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }
        public  async Task<ResultDTO<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserByEmailQuery(request.Email));
            var user = result.Data;
            if (!result.IsSuccess)
                {
                return ResultDTO<bool>.Faliure(ErrorCode.EmailIsNotFound, "Email is Not Found");
                }
            //   if(user.OtpCode!=request.OtpCode || user.OtpExpiry <= DateTime.Now || user.OtpExpiry==null)
            //{
            //    return ResultDTO<bool>.Faliure(ErrorCode.WrongOtp, "Wrong Otp");
            //}
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                return ResultDTO<bool>.Faliure(ErrorCode.PasswordsDontMatch, "Passwords don't match");
            }
            var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.Password = newHashedPassword;
            user.OtpCode = null;
            user.OtpExpiry = null;
            _userRepository.Update(user);
            _userRepository.SaveChanges();
            return ResultDTO<bool>.Sucess(true);
        }
    }
}
