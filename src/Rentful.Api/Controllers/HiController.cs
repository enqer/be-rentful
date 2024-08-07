using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Rentful.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HiController(ILogger<HiController> logger) : ControllerBase
    {
   
        private readonly ILogger<HiController> _logger = logger;

        [HttpGet]
        public async Task<string> Get()
        {
            return await Task.FromResult("Hello, World!");
        }
    }
}
