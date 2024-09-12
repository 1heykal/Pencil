using Microsoft.AspNetCore.Http;

namespace Pencil.ContentManagement.Application.Responses;

public class BaseResponse<T>
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public T? Data { get; set; }

    public int StatusCode { get; set; } = StatusCodes.Status200OK;
    
    public List<string>? ValidationErrors { get; set; }

    public BaseResponse()
    {
        Success = true;
    }

    public BaseResponse(T data)
    {
        Success = true;
        Data = data;
    }

    public BaseResponse(string message)
    {
        Success = true;
        Message = message;
    }

    public BaseResponse(bool success, string message, int statusCode = StatusCodes.Status400BadRequest)
    {
        Success = success;
        Message = message;
        StatusCode = statusCode;
    }
    
    public BaseResponse(bool success, string message, T? data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public BaseResponse(List<string>? validationErrors)
    {
        Success = false;
        StatusCode = StatusCodes.Status400BadRequest;
        ValidationErrors = validationErrors;
    }

    public BaseResponse(List<string>? validationErrors, int statusCode = StatusCodes.Status400BadRequest)
    {
        Success = false;
        ValidationErrors = validationErrors;
        StatusCode = statusCode;
    }
}