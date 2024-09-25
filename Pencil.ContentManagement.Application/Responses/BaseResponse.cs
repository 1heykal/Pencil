using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Resources;

namespace Pencil.ContentManagement.Application.Responses;

public class BaseResponse<T>
{
    public bool Success { get; set; } = true;

    public string Message { get; set; } = string.Empty;

    public T? Data { get; set; }

    public int StatusCode { get; set; } = StatusCodes.Status200OK;
    
    public List<string>? ValidationErrors { get; set; }

    public BaseResponse()
    {
        Message = Shared.Success;
    }

    public BaseResponse(T data)
    {
        Data = data;
        Message = Shared.Success;
    }

    public BaseResponse(string message)
    {
        Message = message;
    }
    
    public BaseResponse(string message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
        Success = statusCode < 400;
    }

    public BaseResponse(bool success, string message, int statusCode = StatusCodes.Status400BadRequest)
    {
        Success = success;
        Message = message;
        StatusCode = statusCode;
    }
    
    public BaseResponse(string message, T data, int statusCode = StatusCodes.Status200OK)
    {
        Message = message;
        Data = data;
        StatusCode = statusCode;
    }

    public BaseResponse(List<string> validationErrors)
    {
        Success = false;
        Message = Shared.Failure;
        StatusCode = StatusCodes.Status400BadRequest;
        ValidationErrors = validationErrors;
    }

    public BaseResponse(List<string> validationErrors, int statusCode)
    {
        Success = false;
        Message = Shared.Failure;
        ValidationErrors = validationErrors;
        StatusCode = statusCode;
    }
}