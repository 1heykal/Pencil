using Microsoft.AspNetCore.Mvc.Filters;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.API.Filters;

public class GetUserIdAttribute : ActionFilterAttribute
{    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var success = Guid.TryParse(context.HttpContext.User?.FindFirst("UserId")?.Value, out var result);

        if (!success)
            context.Result = new UnauthorizedResponse<string>();
        
        context.HttpContext.Items["UserId"] = result;
    }
}