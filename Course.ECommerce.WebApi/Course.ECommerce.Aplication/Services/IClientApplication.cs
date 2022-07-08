using Course.ECommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Aplication.Services
{
    public interface IClientApplication
    {
        Task<Client> GetByIdAsync(string DNI);
    }
}
