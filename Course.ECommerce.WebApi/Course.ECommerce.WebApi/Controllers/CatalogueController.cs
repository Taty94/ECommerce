using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Course.ECommerce.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogueController : ControllerBase, ICatalogueApplication
    {
        private readonly ICatalogueApplication catalogueApplication;

        public CatalogueController(ICatalogueApplication catalogueApplication)
        {
            this.catalogueApplication = catalogueApplication;
        }

        [HttpGet]
        public Task<ICollection<Catalogue>> GetAsync()
        {
            return catalogueApplication.GetAsync();
        }
    }
}
