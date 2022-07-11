using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Domain.Entities;

namespace Course.ECommerce.Aplication.Services
{
    public interface IProductTypeApplication
    {
        Task<ICollection<ProductTypeDto>> GetAsync();
        Task<ProductTypeDto> GetByIdAsync(string id);
        Task<ProductTypeDto> InsertAsync(CreateProductTypeDto productTypeDto);
        Task<ProductTypeDto> UpdateAsync(string id, CreateProductTypeDto productTypeDto);
        Task<bool> DeleteAsync(string id);
        Task<ResultPagination<ProductTypeDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Description", string order = "asc");
    }
}
