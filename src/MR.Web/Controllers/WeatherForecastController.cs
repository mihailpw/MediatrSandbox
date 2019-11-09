using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MR.Web.Features.Weather;

namespace MR.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _mediator.Send(new GetForecast.Command(), HttpContext.RequestAborted));
        }

        [HttpGet("slow")]
        public async Task<IActionResult> GetSlowAsync()
        {
            return Ok(await _mediator.Send(new GetForecastSlow.Command(), HttpContext.RequestAborted));
        }
    }
}
