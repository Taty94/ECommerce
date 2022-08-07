using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Course.ECommerce.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ProductController : ControllerBase, IProductApplication
    {
        private readonly IProductApplication productApp;

        public ProductController(IProductApplication productApp)
        {
            this.productApp = productApp;
        }

        [HttpGet ("all")]
        public async Task<ICollection<ProductDto>> GetAllAsync()
        {
            return await productApp.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<OneProductDto> GetOneByIdAsync(Guid id)
        {
            return await productApp.GetOneByIdAsync(id);
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

        
        [HttpGet]
        public async Task<ResultPagination<ProductDto>> GetListAsync(string? search = "", string? brandId = "", string? typeId = "", int offset = 0, int limit = 3, string sort = "Name", string order = "asc")
        {
            return await productApp.GetListAsync(search,brandId, typeId, offset,limit,sort,order);
        }
    }
}
