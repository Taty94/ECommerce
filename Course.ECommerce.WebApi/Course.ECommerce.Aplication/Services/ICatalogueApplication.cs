using Course.ECommerce.Domain;

namespace Course.ECommerce.Aplication.Services
{
    public interface ICatalogueApplication
    {
        Task<ICollection<Catalogue>> GetAsync();
    }
}