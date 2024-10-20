

using Hangfire;
using InventoryManagementSystem.CQRS.Products.Commands;
using InventoryManagementSystem.CQRS.Products.Queries;
using InventoryManagementSystem.CQRS.Users.Commands;
using InventoryManagementSystem.DTOs.ProductDtos;
using InventoryManagementSystem.Enums;
using InventoryManagementSystem.Exceptions;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.ViewModels;
using InventoryManagementSystem.ViewModels.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResultViewModel<bool>> AddProduct(AddProductViewModel viewModel)
        {
            var productDto = viewModel.MapOne<AddProductDto>();
            var command = new AddProductCommand(productDto);
            var result = await _mediator.Send(command);

            BackgroundJob.Enqueue(() => Debug.WriteLine("Product Created"));
            BackgroundJob.Schedule(() => Debug.WriteLine("Product Created"),TimeSpan.FromMinutes(1));

            RecurringJob.AddOrUpdate("First Job",() => Debug.WriteLine("this is my job"),
                Cron.Daily);

           string parentjobId= BackgroundJob.Enqueue(() => Debug.WriteLine("Product Created"));

            BackgroundJob.ContinueJobWith(parentjobId, () => Debug.WriteLine("Child 1 executed"));
            BackgroundJob.ContinueJobWith(parentjobId, () => Debug.WriteLine("Child 2 executed"));
            return ResultViewModel<bool>.Sucess(true);
        }
        [HttpPut]
           
        public async Task<ResultViewModel<bool>> UpdateProduct(UpdateProductViewModel viewModel)
        {
            var productDto = viewModel.MapOne<UpdateProductDto>();
            var command = new UpdateProductCommand(productDto);
            var result = await _mediator.Send(command);

            return ResultViewModel<bool>.Sucess(true);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            if (!result.IsSuccess)

                throw new BusinessException(result.ErrorCode, result.Message);

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            //if (!result.IsSuccess)

            //    throw new BusinessException(result.ErrorCode, result.Message);

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ResultViewModel<bool>> DeleteProductAsync(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));

            if (!result)
                return ResultViewModel<bool>.Faliure(ErrorCode.UnKnown, "Failed to delete Product or Product not found");

            return ResultViewModel<bool>.Sucess(result, "Product deleted successfully");
        }
    }
}

