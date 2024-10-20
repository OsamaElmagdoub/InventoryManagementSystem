using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.Base;
using MediatR;

namespace InventoryManagementSystem.CQRS.Products.Commands
{
    public record DeleteProductCommand(int Id) :IRequest<bool>;
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IRepository<Product> _productRepository;

        public DeleteProductCommandHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var category = _productRepository.GetByID(request.Id);
            if (category == null)
                return false;

            category.Deleted = true;

            await Task.Run(() =>
            {
                _productRepository.Update(category);
                _productRepository.SaveChanges();
            });

            return true;
        }
    }
}
