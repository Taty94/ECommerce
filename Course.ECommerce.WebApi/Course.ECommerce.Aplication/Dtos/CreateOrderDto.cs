namespace Course.ECommerce.Aplication.Dtos
{
    public class CreateOrderDto
    {
        public string UserEmail { get; set; }
        public string BasketId { get; set; }
        public string DeliveryId { get; set; }
    }
}