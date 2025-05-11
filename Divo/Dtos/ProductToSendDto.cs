using DAL.Entities;

namespace PL.Divo.Dtos
{
    public class ProductToSendDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string PictureUrl { get; set; } = string.Empty;

        public int UnitsInStock { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

    }
}
