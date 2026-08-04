using JobPortalAPI.Core.Enums;

namespace JobPortalAPI.Core.Helpers;

public class ServiceResult<T>
{
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public ServiceErrorType? ErrorType { get; init; }
    public string? ErrorMessage { get; init; }

    public static ServiceResult<T> Ok(T data) => new() { IsSuccess = true, Data = data };
    public static ServiceResult<T> Fail(ServiceErrorType type, string message)
        => new() { IsSuccess = false, ErrorType = type, ErrorMessage = message };
}