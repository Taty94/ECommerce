using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Domain.Entities
{
    public class User
    {
        public string UserName { get; set; }
        public Role[] Role { get; set; }
        public bool Licencia { get; set; }
        public bool Ecuatoriano { get; set; }
        public string CodigoSeguro { get; set; }
    }

    public enum Role
    {
        Admin,
        User,
        Invitado,
        Soporte
    }
}
