using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pencil.ContentManagement.Application.Responses;

public class UnauthorizedResponse<T> : BaseResponse<T>, IActionResult
{
    public UnauthorizedResponse() : base(["You are not Authorized to do this action."], StatusCodes.Status401Unauthorized)
    {
        
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(this)
        {
            StatusCode = StatusCode
        };

        await objectResult.ExecuteResultAsync(context);
    }
}