namespace Course.ECommerce.Aplication.Services
{
    public class OrderDetailedDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<ItemOrderedDto> ItemsOrdered { get; set; }
        public string Status { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public DateTime CreationDate { get; set; }

    }
}