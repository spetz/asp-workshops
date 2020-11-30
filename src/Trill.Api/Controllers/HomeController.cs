using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Trill.Application.Services;
using Trill.Infrastructure;

namespace Trill.Api.Controllers
{
    [Route("")]
    public class HomeController : BaseController
    {
        private readonly IServiceId _serviceId;
        private readonly IOptions<ApiOptions> _apiOptions;

        public HomeController(IServiceId serviceId, IOptions<ApiOptions> apiOptions)
        {
            _serviceId = serviceId;
            _apiOptions = apiOptions;
        }
        
        [HttpGet]
        public ActionResult<string> Get() => _apiOptions.Value.Name;

        [HttpGet("id")]
        public ActionResult<string> GetId() => _serviceId.GetId();
    }
}