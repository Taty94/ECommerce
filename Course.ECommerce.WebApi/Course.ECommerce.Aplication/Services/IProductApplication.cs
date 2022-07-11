using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;

namespace Course.ECommerce.Aplication.Services
{
    public interface IProductApplication
    {
        //Task<ICollection<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetByIdAsync(Guid Id);
        Task<ProductDto> InsertAsync(CreateProductDto product);
        Task<ProductDto> UpdateAsync(Guid id, CreateProductDto product);
        Task<bool> DeleteAsync(Guid Id);
        Task<ResultPagination<ProductDto>> GetListAsync(string? search="", int offset=0, int limit = 3, string sort="Name", string order="asc");
    }
}