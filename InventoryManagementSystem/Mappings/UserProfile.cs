using AutoMapper;
using InventoryManagementSystem.CQRS.Users.Commands;
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModels.Users;


namespace InventoryManagementSystem.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserViewModel, RegisterUserCommand>();
            CreateMap<RegisterUserCommand, User>();
            CreateMap<ResetPasswordViewModel, ResetPasswordCommand>();
            CreateMap<User,UserDto>().ReverseMap();
        }
    }
}
