using System;
using Microsoft.AspNetCore.Mvc;

namespace Trill.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private Guid _userId;

        protected Guid UserId
        {
            get
            {
                if (_userId != Guid.Empty)
                {
                    return _userId;
                }

                if (User.Identity.IsAuthenticated)
                {
                    _userId = Guid.Parse(User.Identity.Name ?? Guid.Empty.ToString());
                }

                return _userId;
            }
        }
    }
}