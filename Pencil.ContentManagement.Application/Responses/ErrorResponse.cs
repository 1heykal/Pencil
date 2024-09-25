namespace Pencil.ContentManagement.Application.Responses;

public class ErrorResponse<T> : BaseResponse<T>
{
    public ErrorResponse(string message, int statusCode) : base(false, message, statusCode)
    {
        
    }
    
}