
using InventoryManagementSystem.CQRS.Users.Commands;
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Exceptions;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.ViewModels;
using InventoryManagementSystem.ViewModels.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<RegisterUserCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode,result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
        }

        [HttpPut]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<ResetPasswordCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPasswordAsync(string email)
        {
            var result = await _mediator.Send(new ForgetPasswordCommand(email));
            if (!result.IsSuccess)
                return BadRequest("User not found or invalid request.");

            return Ok(ResultViewModel<string>.Sucess(result, "Password reset link has been sent."));
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLoginDto user)
        {
            var x = await _mediator.Send(new LoginUserCommand(user));

            return Ok(x);
        }
    }
}

