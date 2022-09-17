using Microsoft.AspNetCore.Mvc;

namespace Api.Core
{
    [ApiController]
    public abstract class ApiController : Controller
    {
        protected new IActionResult Response(object result = null)
        {
            return Ok(new
            {
                success = true,
                data = result
            });            
        }
    }
}