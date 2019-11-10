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
        public async Task<GetForecast.Response> GetAsync()
        {
            return await _mediator.Send(new GetForecast.Request(), HttpContext.RequestAborted);
        }

        [HttpGet("slow")]
        public async Task<GetForecastSlow.Response> GetSlowAsync()
        {
            return await _mediator.Send(new GetForecastSlow.Request(), HttpContext.RequestAborted);
        }
    }
}
