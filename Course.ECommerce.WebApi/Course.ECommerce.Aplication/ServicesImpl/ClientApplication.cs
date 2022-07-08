using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Aplication.ServicesImpl
{
    public class ClientApplication : IClientApplication
    {
        protected IClientRepository RepoClient { get; set; }

        public ClientApplication(IClientRepository repoClient)
        {
            RepoClient = repoClient;
        }

        public Task<Client> GetByIdAsync(string DNI)
        {
            return RepoClient.GetByIdAsync(DNI);
        }
    }
}
