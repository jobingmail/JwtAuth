using Microsoft.AspNetCore.Mvc;

namespace JwtAuthAPI.Controllers
{
    [Route("api")]
    public class MyController : ControllerBase
    {
        [HttpGet]
        [Route("getBaseUrl")]
        public string Index()
        {
            var request = HttpContext.Request;

            var baseUrl = $"{request.Scheme}://{request.Host}:{request.PathBase.ToUriComponent()}";

            return baseUrl;
        }
    }
}
