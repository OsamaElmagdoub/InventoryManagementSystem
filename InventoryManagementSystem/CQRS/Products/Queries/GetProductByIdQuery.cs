
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Enums;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Base;
using MediatR;

namespace InventoryManagementSystem.CQRS.Products.Queries
{
    public record GetProductByIdQuery(int id) : IRequest<ResultDTO<Product>>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ResultDTO<Product>>
    {
        private readonly IRepository<Product> _productRepository;

        public GetProductByIdQueryHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ResultDTO<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = _productRepository.GetByID(request.id);

            if (product is null)
            {
                return ResultDTO<Product>.Faliure(ErrorCode.UnKnown, "Invalid Product Id");
            }
            return ResultDTO<Product>.Sucess(product);
        }
    }
}



        
    

