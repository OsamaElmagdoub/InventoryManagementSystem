
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Enums;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Base;
using MediatR;

namespace InventoryManagementSystem.CQRS.Users.Queries
{
    public record IsEmailExistQuery(string email) : IRequest<ResultDTO<bool>>;
    public class IsEmailExistQueryHandler : IRequestHandler<IsEmailExistQuery, ResultDTO<bool>>
    {
        private readonly IRepository<User> _userRepository;
        public IsEmailExistQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultDTO<bool>> Handle(IsEmailExistQuery request, CancellationToken cancellationToken)
        {
            var result = await Task.Run(() => _userRepository.Any(u => u.Email == request.email));
            if (result)
            {
                return ResultDTO<bool>.Sucess(true);
            }
            return ResultDTO<bool>.Faliure(ErrorCode.EmailIsNotFound, "Email is Not Found");
        }
    }
}
