using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Course.ECommerce.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class ProductBrandController : ControllerBase, IProductBrandApplication
    {
        private readonly IProductBrandApplication productBrandApp;

        public ProductBrandController(IProductBrandApplication productBrandApp)
        {
            this.productBrandApp = productBrandApp;
        }

        [HttpGet]
        public async Task<ICollection<ProductBrandDto>> GetAsync()
        {
            return await productBrandApp.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<ProductBrandDto> GetByIdAsync(string id)
        {
            return await productBrandApp.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ProductBrandDto> InsertAsync(CreateProductBrandDto productBrandDto)
        {
            return await productBrandApp.InsertAsync(productBrandDto);
        }

        [HttpPut]
        public async Task<ProductBrandDto> UpdateAsync(string id, CreateProductBrandDto productBrandDto)
        {
            return await productBrandApp.UpdateAsync(id, productBrandDto);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(string id)
        {
            return await productBrandApp.DeleteAsync(id);
        }

        [HttpGet("pagination")]
        public async  Task<ResultPagination<ProductBrandDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Description", string order = "asc")
        {
            return await productBrandApp.GetListAsync(search, offset, limit, sort, order);
        }
    }
}
