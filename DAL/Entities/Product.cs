using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Product : BaseEntity
    {
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string PictureUrl { get; set; } = string.Empty;

        public int UnitsInStock { get; set; } 

        public bool IsDeleted { get; set; } = false;

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public int BrandId { get; set; }

        public Brand? Brand { get; set; }

    }
}
