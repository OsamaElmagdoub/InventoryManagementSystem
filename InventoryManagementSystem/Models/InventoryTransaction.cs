using InventoryManagementSystem.Enums;

namespace InventoryManagementSystem.Models
{
    public class InventoryTransaction : BaseModel
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
