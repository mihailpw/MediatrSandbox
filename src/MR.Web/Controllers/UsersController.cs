using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MR.Web.Features.Users;

namespace MR.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllUsers.Request request)
        {
            var response = await _mediator.Send(request, HttpContext.RequestAborted);
            return Ok(response.Users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] GetUserById.Request request)
        {
            var response = await _mediator.Send(request, HttpContext.RequestAborted);
            return Ok(response.User);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUser.Request request)
        {
            var response = await _mediator.Send(request, HttpContext.RequestAborted);
            return Created($"users/{response.User.Id}", response.User);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] DeleteUserById.Request request)
        {
            await _mediator.Send(request, HttpContext.RequestAborted);
            return Ok();
        }
    }
}