﻿namespace InventoryManagementSystem.DTOs.ProductDtos
{
    public class ReturnProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool LowStockThreshold { get; set; } = false;
    }
}
