namespace InventoryManagementSystem.Models
{
    public class StockTransfer : BaseModel
    {
        public int Quantity { get; set; }
        public DateTime TransferDate { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Warehouse Warehouse { get; set; }
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
