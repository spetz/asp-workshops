using Microsoft.AspNetCore.Mvc;
using Trill.Application.Services;

namespace Trill.Api.Controllers
{
    [Route("")]
    public class HomeController : BaseController
    {
        private readonly IServiceId _serviceId;

        public HomeController(IServiceId serviceId)
        {
            _serviceId = serviceId;
        }
        
        [HttpGet]
        public ActionResult<string> Get() => "Trill";

        [HttpGet("id")]
        public ActionResult<string> GetId() => _serviceId.GetId();
    }
}