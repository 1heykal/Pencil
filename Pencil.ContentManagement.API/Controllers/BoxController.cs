using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.Boxes.Commands;
using Pencil.ContentManagement.Application.Features.Boxes.Queries;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoxController : ControllerBase
{
    private readonly IMediator _mediator;

    public BoxController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<BoxDto>>> GetBox()
    {
        var response = await _mediator.Send(new GetBoxQuery());
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost("Save")]
    public async Task<ActionResult<BaseResponse<string>>> SavePost(SavePostToBoxCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpDelete("UnSave/{id}")]
    public async Task<ActionResult<BaseResponse<string>>> UnSavePost(Guid id)
    {
        var response = await _mediator.Send(new UnSavePostToBoxCommand{PostId = id});
        return StatusCode(response.StatusCode,
            response.StatusCode is StatusCodes.Status204NoContent ? null : response);
    }
}