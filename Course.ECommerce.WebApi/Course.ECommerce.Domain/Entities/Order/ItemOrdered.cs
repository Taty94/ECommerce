using Course.ECommerce.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Domain.Entities.Order
{
    public class ItemOrdered : BaseCatalogueEntity
    {
        public ItemOrdered()
        {
        }

        public ItemOrdered(string name, decimal price, int quantiy)
        {
            Name = name;
            Price = price;
            Quantiy = quantiy;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantiy { get; set; }
    }
}
