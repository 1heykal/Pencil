using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.Auth.Commands.AuthenticateUser;
using Pencil.ContentManagement.Application.Features.Auth.Commands.ChangePassword;
using Pencil.ContentManagement.Application.Features.Auth.Commands.RegisterUser;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<BaseResponse<string>>> Login(AuthenticateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<BaseResponse<string>>> Register(RegisterUserCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpPut("ChangePassword")][Authorize]
        public async Task<ActionResult<BaseResponse<string>>> ChangePassword(ChangePasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return  StatusCode(response.StatusCode, response.StatusCode is StatusCodes.Status204NoContent? null : response);
        }
    }
}
