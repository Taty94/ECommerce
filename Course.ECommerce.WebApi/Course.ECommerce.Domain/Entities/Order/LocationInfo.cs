using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Domain.Entities.Order
{
    public class LocationInfo
    {
        public LocationInfo()
        {
        }

        public LocationInfo(string fullName, string email, string adress, string city, string phone)
        {
            FullName = fullName;
            Email = email;
            Adress = adress;
            City = city;
            Phone = phone;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
    }
}
