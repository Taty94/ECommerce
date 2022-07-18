using Course.ECommerce.Domain.Entities.Order;

namespace Course.ECommerce.Aplication.Dtos { 

    public class CreateLocationInfoDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MainStreet { get; set; }
        public string SecondaryStreet { get; set; }
        public string City { get; set; }   
        public string Phone { get; set; }   
    }
}