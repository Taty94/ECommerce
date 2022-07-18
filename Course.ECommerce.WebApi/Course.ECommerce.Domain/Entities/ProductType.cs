using Course.ECommerce.Domain.Base;

namespace Course.ECommerce.Domain.Entities
{
    public class ProductType : BaseCatalogueEntity
    {
        public ProductType()
        {
            this.CreationDate=DateTime.Now;
        }

        public string Description { get; set; }
    }
}
