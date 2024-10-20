


using AutoMapper;
using InventoryManagementSystem.DTOs.ProductDtos;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModels.Products;

namespace InventoryManagementSystem.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddProductViewModel , AddProductDto>();
            CreateMap<Product , AddProductDto>().ReverseMap();
            CreateMap<UpdateProductViewModel , UpdateProductDto>();
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            CreateMap<ReturnProductDto, Product>().ReverseMap();
            CreateMap<ReturnProductViewModel, ReturnProductDto>();



        }
    }
}
