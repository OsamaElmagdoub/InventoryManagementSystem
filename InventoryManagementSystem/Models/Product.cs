namespace InventoryManagementSystem.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool LowStockThreshold { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public ICollection<WarehouseProduct> WarehouseProducts { get; set; } = new HashSet<WarehouseProduct>();

    }
}
