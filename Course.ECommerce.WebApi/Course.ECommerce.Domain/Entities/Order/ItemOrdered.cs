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
            this.CreationDate = DateTime.Now;
        }

        public ItemOrdered(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }

    }
}
