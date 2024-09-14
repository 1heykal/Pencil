using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.Account.Commands.UpdateProfile;

namespace Pencil.ContentManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPut("Profile")][Authorize]
    public async Task<ActionResult> UpdateProfile(UpdateProfileCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response.StatusCode is StatusCodes.Status204NoContent? null : response);
    }
}