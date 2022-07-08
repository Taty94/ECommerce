using Course.ECommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Infrastructure
{
    public class ClientRepository : IClientRepository
    {
        public Task<Client> GetByIdAsync(string DNI)
        {
            var client = new Client { Id = 1721795126, Name = "Bar", LastName = "Foo" };
            //Programacion Asincrona
            //Se devuelve siempre una tarea
            //await - espera hasta que la consulta se ejecute
            //el error es que no retorno una tarea
            //solo utilizar asincrono cuando sea necesario
            return Task.FromResult(client);
        }
    }
}
