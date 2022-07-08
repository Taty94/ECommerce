using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;

namespace Course.ECommerce.Aplication.Services
{
    public interface IProductApplication
    {
        //Task<ICollection<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(Guid Id);
        Task<ProductDto> PostAsync(CreateProductDto product);
        Task<ProductDto> PutAsync(Guid id, CreateProductDto product);
        Task<bool> DeleteAsync(Guid Id);
        Task<ResultPagination<ProductDto>> GetListAsync(string? search="", int offset=0, int limit = 10, string sort="Name", string order="asc");
    }
}