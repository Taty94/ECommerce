namespace Course.ECommerce.Domain
{
    public interface ICatalogueRepository
    {
        Task<ICollection<Catalogue>> GetAsync();
    }
}