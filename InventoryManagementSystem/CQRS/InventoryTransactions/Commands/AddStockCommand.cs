//using InventoryManagementSystem.CQRS.Products.Queries;
//using InventoryManagementSystem.DTOs.InventoryTransactionDtos;
//using InventoryManagementSystem.Helpers;
//using InventoryManagementSystem.Models;
//using InventoryManagementSystem.Repositories.Base;
//using MediatR;

//namespace InventoryManagementSystem.CQRS.InventoryTransactions.Commands
//{
//    public record AddStockCommand(AddStockDto addStockDto) : IRequest<bool>;

//    public class AddStockCommandHandler : IRequestHandler<AddStockCommand, bool>
//    {
//        private readonly IRepository<InventoryTransaction> _inventoryrepository;
//        private readonly IMediator _mediator;

//        public AddStockCommandHandler(IRepository<InventoryTransaction> inventoryrepository,IMediator mediator)
//        {
//            _inventoryrepository = inventoryrepository;
//            _mediator = mediator;
//        }
//        public Task<bool> Handle(AddStockCommand request, CancellationToken cancellationToken)
//        {
//            var result =  _mediator.Send(new GetProductByIdQuery(request.addStockDto.ProductId));


//        }
//    }
//}
