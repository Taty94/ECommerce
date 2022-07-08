using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Domain
{
    public interface IClientRepository
    {
        Task<Client> GetByIdAsync(string DNI);
    }
}
