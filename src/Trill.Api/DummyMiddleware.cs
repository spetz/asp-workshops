using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Trill.Api
{
    public class DummyMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            return next(context);
        }
    }
}