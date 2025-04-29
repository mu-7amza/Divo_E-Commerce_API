using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dtos
{
    public class ProductDto
    {

      
        public string Name { get; set; } 

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int UnitsInStock { get; set; }

        public bool IsDeleted { get; set; } = false;

        public int CategoryId { get; set; }




    }
}
