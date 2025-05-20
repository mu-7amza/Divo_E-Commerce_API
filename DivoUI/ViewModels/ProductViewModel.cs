namespace DivoUI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } 

        public string PictureUrl { get; set; } = string.Empty;

        public int UnitsInStock { get; set; }

        public string Category { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;
    }
}
