

using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.DTOs.ProductDtos;
using InventoryManagementSystem.Enums;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Base;
using MediatR;

namespace InventoryManagementSystem.CQRS.Products.Queries
{
    public record GetAllProductsQuery : IRequest<IEnumerable<ReturnProductDto>>;

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ReturnProductDto>>
    {
        private readonly IRepository<Product> _productRepository;

        public GetAllProductsQueryHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
      

        async Task<IEnumerable<ReturnProductDto>> IRequestHandler<GetAllProductsQuery, IEnumerable<ReturnProductDto>>.Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products =  _productRepository.GetAll();
            var productDto = products.Map<ReturnProductDto>();

            return productDto;
        }
    }
}



        
    

