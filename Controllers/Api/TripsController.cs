using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;

namespace WebApplication1.Controllers.Api
{
    public class TripsController : Controller
    {
        [HttpGet("api/trips")]
        public IActionResult Get()
        {
            return Ok(Json(new Trip { Name = "Us Trip"}));
        }
    }
}