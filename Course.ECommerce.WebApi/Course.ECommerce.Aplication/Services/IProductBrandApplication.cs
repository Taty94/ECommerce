using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;

namespace Course.ECommerce.Aplication.Services
{
    public interface IProductBrandApplication
    {
        Task<ICollection<ProductBrandDto>> GetAsync();
        Task<ProductBrandDto> GetByIdAsync(string id);

        Task<ProductBrandDto> PostAsync(CreateProductBrandDto productBrandDto);
        Task<ProductBrandDto> PutAsync(string id,CreateProductBrandDto productBrandDto);
        Task<bool> DeleteAsync(string id);

        Task<ResultPagination<ProductBrandDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Description", string order = "asc");
    }
}
