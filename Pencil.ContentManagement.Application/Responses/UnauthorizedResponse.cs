using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pencil.ContentManagement.Application.Responses;

public class UnauthorizedResponse<T> : BaseResponse<T>
{
    public UnauthorizedResponse() : base(["You are not Authorized to perform this action."], StatusCodes.Status401Unauthorized)
    {
        
    }
}