using Course.ECommerce.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Domain.Entities.Order
{
    public class Order : BaseBusinessEntity
    {
        public Order()
        {
        }

        public Order(string userEmail, ICollection<ItemOrdered> itemsOrdered, Delivery deliveryMethod, 
            decimal subtotal)
        {
            UserEmail = userEmail;
            DeliveryMethod = deliveryMethod;
            ItemsOrdered = itemsOrdered;
            Subtotal = subtotal;
        }

        public string UserEmail { get; set; }
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
        public Delivery DeliveryMethod { get; set; }
        public ICollection<ItemOrdered> ItemsOrdered { get; set; }
        public Status Status { get; set; } = Status.Pendiente;
        public decimal Subtotal { get; set; }

        public decimal CalculateTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }

    public enum Status
    {
        [EnumMember(Value="En camino")]
        Pendiente,
        Recibido,
        [EnumMember(Value = "Orden Fallida")]
        Fallo
    }
}
