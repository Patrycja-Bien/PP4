namespace EShopService.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Ean { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Stock { get; set; } = 0;

        public string Sku { get; set; } = string.Empty;

        public Category Category { get; set; }

        public bool Deleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Guid UpdatedBy { get; set; }
    }
}
