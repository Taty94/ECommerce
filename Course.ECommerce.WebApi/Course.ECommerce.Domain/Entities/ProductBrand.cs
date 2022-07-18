using Course.ECommerce.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Domain.Entities
{
    public class ProductBrand : BaseCatalogueEntity
    {
        public ProductBrand()
        {
            this.CreationDate=DateTime.Now;
        }

        public string Description { get; set; }
    }
}
