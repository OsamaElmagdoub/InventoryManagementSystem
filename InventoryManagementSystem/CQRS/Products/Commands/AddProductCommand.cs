
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.DTOs.ProductDtos;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Base;
using MediatR;

namespace InventoryManagementSystem.CQRS.Products.Commands
{
    public record AddProductCommand(AddProductDto addProductDto) : IRequest<ResultDTO<bool>>;

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ResultDTO<bool>>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMediator _mediator;

        public AddProductCommandHandler(IRepository<Product> productRepository, IMediator mediator)
        {
            _productRepository = productRepository;
            _mediator = mediator;
        }
        public async Task<ResultDTO<bool>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.addProductDto.MapOne<Product>();
            await _productRepository.AddAsync(product);
            //if (request.addProductDto.Quantity < 5)
            //{
            //    request.addProductDto.LowStockThreshold = true;
            //   // _productRepository.SaveChanges();

            //}
            _productRepository.SaveChanges();
            return ResultDTO<bool>.Sucess(true);
        }
    }
}
