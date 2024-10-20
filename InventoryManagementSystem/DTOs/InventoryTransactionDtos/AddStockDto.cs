using InventoryManagementSystem.Enums;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DTOs.InventoryTransactionDtos
{
    public class AddStockDto
    {
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public TransactionType TransactionType { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
