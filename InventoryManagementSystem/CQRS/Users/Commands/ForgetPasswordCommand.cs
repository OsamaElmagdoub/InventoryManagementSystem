
using InventoryManagementSystem.CQRS.Users.Queries;
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Base;
using MediatR;

namespace InventoryManagementSystem.CQRS.Users.Commands
{
    public record ForgetPasswordCommand(string Email) : IRequest<ResultDTO<bool>>;

    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ResultDTO<bool>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMediator _mediator;
        private readonly IFluentEmailService _emailService;

        public ForgetPasswordCommandHandler(IRepository<User> userRepository, IMediator mediator, IFluentEmailService emailService)
        {
            _userRepository = userRepository;
            _mediator = mediator;
            _emailService = emailService;
        }

        public async Task<ResultDTO<bool>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new IsEmailExistQuery(request.Email));
            if (!result.IsSuccess)
                return result;

            var resetUrl = $"https://localhost:7025/reset-password&email={request.Email}";
            var email = new SendEmailDto(request.Email, "Password Reset", $"Click to reset your password: {resetUrl}");
            await _emailService.SendEmailAsync(email);

            return ResultDTO<bool>.Sucess(true);
        }
    }
}
