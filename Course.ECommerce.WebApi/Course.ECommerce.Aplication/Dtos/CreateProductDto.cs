using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Aplication.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ProductTypeId { get; set; }
        public string ProductBrandId { get; set; }
    }
}
