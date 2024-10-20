
using InventoryManagementSystem.CQRS.Products.Queries;
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.DTOs.ProductDtos;
using InventoryManagementSystem.Enums;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Base;
using MediatR;

namespace InventoryManagementSystem.CQRS.Products.Commands
{
    public record UpdateProductCommand(UpdateProductDto updateProductDto) : IRequest<ResultDTO<bool>>;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResultDTO<bool>>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMediator _mediator;

        public UpdateProductCommandHandler(IRepository<Product> productRepository, IMediator mediator)
        {
            _productRepository = productRepository;
            _mediator = mediator;
        }
        public async Task<ResultDTO<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(request.updateProductDto.Id));

            if (!result.IsSuccess)
            {
                return ResultDTO<bool>.Faliure(ErrorCode.UnKnown, "NotFound");
            }
            var product = result.Data;

            product = request.updateProductDto.MapOne<Product>();
            _productRepository.SaveChanges();

            _productRepository.Update(product);
            _productRepository.SaveChanges();

            return ResultDTO<bool>.Sucess(true, "Product updated Successfully!");



        }
    }
}
