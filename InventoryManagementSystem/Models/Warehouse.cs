namespace InventoryManagementSystem.Models
{
    public class Warehouse : BaseModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<WarehouseProduct> WarehouseProducts { get; set; } = new HashSet<WarehouseProduct>();

    }
}
