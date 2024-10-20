namespace InventoryManagementSystem.Models
{
    public class WarehouseProduct
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Warehouse Warehouse { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
    }
}
