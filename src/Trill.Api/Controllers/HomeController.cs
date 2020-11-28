using Microsoft.AspNetCore.Mvc;

namespace Trill.Api.Controllers
{
    [Route("")]
    public class HomeController : BaseController
    {

        [HttpGet]
        public ActionResult<string> Get() => "Trill";
    }
}