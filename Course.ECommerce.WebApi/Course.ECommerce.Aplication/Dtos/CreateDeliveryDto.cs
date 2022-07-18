namespace Course.ECommerce.Aplication.Dtos
{
    public class CreateDeliveryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}