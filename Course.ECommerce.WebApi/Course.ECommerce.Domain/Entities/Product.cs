using Course.ECommerce.Domain.Base;

namespace Course.ECommerce.Domain.Entities
{
    public class Product : BaseBusinessEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }
        public string ProductBrandId { get; set; }
        public ProductBrand? ProductBrand { get; set; }

        public Product()
        {
            this.CreationDate = DateTime.Now;
        }
    }
}
