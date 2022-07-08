using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Domain
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
