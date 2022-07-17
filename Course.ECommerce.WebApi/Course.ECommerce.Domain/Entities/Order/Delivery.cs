using Course.ECommerce.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Domain.Entities.Order
{
    public class Delivery : BaseCatalogueEntity
    {
        public string Name { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
