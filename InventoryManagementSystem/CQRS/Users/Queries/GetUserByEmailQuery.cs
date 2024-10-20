
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Enums;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Base;
using MediatR;

namespace InventoryManagementSystem.CQRS.Users.Queries
{
    public record GetUserByEmailQuery(string email) : IRequest<ResultDTO<User>>;

    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, ResultDTO<User>>
    {
        private readonly IRepository<User> _userRepository;

        public GetUserByEmailQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultDTO<User>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = _userRepository.First(u => u.Email == request.email);
            if (user is null)
            {
                return ResultDTO<User>.Faliure(ErrorCode.EmailIsNotFound, "Email is Not Found");
            }
            return ResultDTO<User>.Sucess(user);
        }
    }
}
