using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.Subscriptions.Commands;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SubscriptionController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriptionController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("Subscribe")]
    public async Task<ActionResult<BaseResponse<string>>> Subscribe(SubscribeCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpDelete("Unsubscribe")]
    public async Task<ActionResult<BaseResponse<string>>> Unsubscribe(UnsubscribeCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode,
            response.StatusCode is StatusCodes.Status204NoContent ? null : response);
    }
}