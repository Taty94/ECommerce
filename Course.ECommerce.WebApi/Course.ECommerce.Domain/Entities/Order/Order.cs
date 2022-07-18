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

        public Order(string userEmail,LocationInfo locationInfo, ICollection<ItemOrdered> itemsOrdered, Delivery deliveryMethod, 
            decimal subtotal)
        {
            UserEmail = userEmail;
            LocationInfo = locationInfo;
            DeliveryMethod = deliveryMethod;
            ItemsOrdered = itemsOrdered;
            Subtotal = subtotal;
            this.CreationDate = DateTime.Now;
        }

        public string UserEmail { get; set; }
        public LocationInfo LocationInfo { get; set; }
        public Delivery? DeliveryMethod { get; set; }
        public string DeliveryMethodId { get; set; }
        public ICollection<ItemOrdered> ItemsOrdered { get; set; }
        public Status Status { get; set; } = Status.Pendiente;
        public decimal Subtotal { get; set; }

        public decimal GetTotal()
        {
            return DeliveryMethod!=null? Subtotal + DeliveryMethod.Price:0;
        }
    }

    public enum Status
    {
        [EnumMember(Value="En camino")]
        Pendiente,
        Recibido,
        [EnumMember(Value = "Orden Cancelada")]
        Cancelar
    }
}
