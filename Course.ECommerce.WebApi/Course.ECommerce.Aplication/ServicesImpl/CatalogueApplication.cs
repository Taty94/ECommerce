using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain;
using Course.ECommerce.Infrastructure;

namespace Course.ECommerce.Aplication.ServicesImpl
{
    /// <summary>
    /// Servicio de aplicacion, para los catalogos de productos
    /// </summary>
    public class CatalogueApplication : ICatalogueApplication
    {
        protected ICatalogueRepository CatalogueRepository { get; set; }

        public CatalogueApplication(ICatalogueRepository repoCatalogue)
        {
            CatalogueRepository = repoCatalogue;
        }

        public async Task<ICollection<Catalogue>> GetAsync()
        {
            return await CatalogueRepository.GetAsync();
        }
    }


}