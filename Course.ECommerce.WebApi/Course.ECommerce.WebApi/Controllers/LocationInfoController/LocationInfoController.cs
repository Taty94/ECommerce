using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.ECommerce.WebApi.Controllers.LocationInfoController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationInfoController : ControllerBase, ILocationInfoApplication
    {
        private readonly ILocationInfoApplication locationInfoApp;

        public LocationInfoController(ILocationInfoApplication locationInfoApp)
        {
            this.locationInfoApp = locationInfoApp;
        }

        [HttpGet]
        public async Task<LocationInfo> GetLocationInfoAsync(string email)
        {
            return await locationInfoApp.GetLocationInfoAsync(email);
        }

        [HttpPost]
        public async Task<LocationInfo> UpdateLocationInfoAsync(LocationInfo LocationInfo)
        {
            return await locationInfoApp.UpdateLocationInfoAsync(LocationInfo);
        }

        [HttpDelete]
        public async Task<bool> DeleteLocationInfoAsync(string email)
        {
            return await locationInfoApp.DeleteLocationInfoAsync(email);
        }
    }
}
