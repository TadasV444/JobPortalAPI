namespace JobPortalAPI.Api.Extentions;

public class ApiResponse<T>(bool success, int statusCode, string message, T data, List<string>? errors = null)
{
  public bool Success { get; set; } = success;
  public int StatusCode { get; set; } = statusCode;
  public string Message { get; set; } = message;
  public T Data { get; set; } = data;
  public List<string> Errors = errors ?? [];
  public DateTime Timestamp { get; set; } = DateTime.UtcNow;

  public static ApiResponse<T> CreateSuccess(T data, string message = "Operation successful.", int statusCode = 200)
  {
    return new ApiResponse<T>(true, statusCode, message, data);
  }

  public static ApiResponse<T?> CreateFailure(string message, int statusCode, List<string>? errors = null)
  {
    return new ApiResponse<T?>(false, statusCode, message, default,  errors);
  }
}