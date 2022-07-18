using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Course.ECommerce.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase, IProductApplication
    {
        private readonly IProductApplication productApp;

        public ProductController(IProductApplication productApp)
        {
            this.productApp = productApp;
        }

        [HttpGet]
        public async Task<ICollection<ProductDto>> GetAllAsync()
        {
            return await productApp.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            return await productApp.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ProductDto> InsertAsync(CreateProductDto productDto)
        {
            return await productApp.InsertAsync(productDto);
        }

        [HttpPut]
        public async Task<ProductDto> UpdateAsync(Guid id, CreateProductDto productDto)
        {
            return await productApp.UpdateAsync(id,productDto);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await productApp.DeleteAsync(id);
        }

        
        [HttpGet("pagination")]
        public async Task<ResultPagination<ProductDto>> GetListAsync(string? search = "", int offset = 0, int limit = 3, string sort = "Name", string order = "asc")
        {
            return await productApp.GetListAsync(search,offset,limit,sort,order);
        }
    }
}
