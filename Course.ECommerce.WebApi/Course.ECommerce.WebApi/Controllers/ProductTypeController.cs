using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course.ECommerce.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductTypeController : ControllerBase, IProductTypeApplication
    {
        private readonly IProductTypeApplication productTypeApp;

        public ProductTypeController(IProductTypeApplication productTypeApp)
        {
            this.productTypeApp = productTypeApp;
        }

        [HttpGet]
        public async Task<ICollection<ProductTypeDto>> GetAsync()
        {
            return await productTypeApp.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<ProductTypeDto> GetByIdAsync(string id)
        {
            return await productTypeApp.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ProductTypeDto> InsertAsync(CreateProductTypeDto productTypeDto)
        {
            return await productTypeApp.InsertAsync(productTypeDto);
        }

        [HttpPut]
        public async Task<ProductTypeDto> UpdateAsync(string id, CreateProductTypeDto productTypeDto)
        {
            return await productTypeApp.UpdateAsync(id, productTypeDto);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(string id)
        {
            return await productTypeApp.DeleteAsync(id);
        }

        [HttpGet("pagination")]
        public async Task<ResultPagination<ProductTypeDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Description", string order = "asc")
        {
            return await productTypeApp.GetListAsync(search, offset, limit, sort, order);
        }
    }
}
