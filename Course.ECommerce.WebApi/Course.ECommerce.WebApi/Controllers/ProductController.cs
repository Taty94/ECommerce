using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Course.ECommerce.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase, IProductApplication
    {
        private readonly IProductApplication productApp;

        public ProductController(IProductApplication productApp)
        {
            this.productApp = productApp;
        }

        //[HttpGet]
        //public async Task<ICollection<ProductDto>> GetProductsAsync()
        //{
        //    return await productApp.GetProductsAsync();
        //}

        [HttpGet("{id}")]
        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            return await productApp.GetProductByIdAsync(id);
        }

        [HttpPost]
        public async Task<ProductDto> PostAsync(CreateProductDto productDto)
        {
            return await productApp.PostAsync(productDto);
        }

        [HttpPut]
        public async Task<ProductDto> PutAsync(Guid id, CreateProductDto productDto)
        {
            return await productApp.PutAsync(id,productDto);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(Guid Id)
        {
            return await productApp.DeleteAsync(Id);
        }

        [HttpGet("pagination")]
        public async Task<ResultPagination<ProductDto>> GetListAsync(string? search = "", int offset = 0, int limit = 3, string sort = "Name", string order = "asc")
        {
            return await productApp.GetListAsync(search,offset,limit,sort,order);

        }
    }
}
