using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.Api.Extentions;

public static class ApiResponseExtention
{
    // BadRequest (400)
    public static ActionResult<ApiResponse<T>> BadRequestResponse<T>(
        this ControllerBase controller,
        string message,
        List<string>? errors = null)
    {
        return controller.BadRequest(
            ApiResponse<T?>.CreateFailure(
                message: message,
                statusCode: StatusCodes.Status400BadRequest,
                errors: errors
            )
        );
    }
    
    // Unauthorized (401)
    public static ActionResult<ApiResponse<T>> UnauthorizedResponse<T>(
        this ControllerBase controller,
        string message,
        List<string>? errors = null)
    {
        return controller.Unauthorized(
            ApiResponse<T?>.CreateFailure(
                message: message,
                statusCode: StatusCodes.Status401Unauthorized,
                errors: errors)
        );
    }
    
    // Forbidden (403)
    public static ActionResult<ApiResponse<T>> ForbiddenResponse<T>(
        this ControllerBase controller,
        string message,
        List<string>? errors = null)
    {
        return controller.StatusCode(
            StatusCodes.Status403Forbidden, // Forbid only accepts Authentication properties
            ApiResponse<T?>.CreateFailure(
                message: message,
                statusCode: StatusCodes.Status403Forbidden,
                errors: errors
            )
        );
    }
    
    // NotFound (404)
    public static ActionResult<ApiResponse<T>> NotFoundResponse<T>(
        this ControllerBase controller,
        string message,
        List<string>? errors = null)
    {
        return controller.NotFound(
            ApiResponse<T?>.CreateFailure(
                message: message,
                statusCode: StatusCodes.Status404NotFound,
                errors: errors
            )
        );
    }
    
    // Conflict (409)
    public static ActionResult<ApiResponse<T>> ConflictResponse<T>(
        this ControllerBase controller,
        string message,
        List<string>? errors = null)
    {
        return controller.Conflict(
            ApiResponse<T?>.CreateFailure(
                message: message,
                statusCode: StatusCodes.Status409Conflict,
                errors: errors
            )
        );
    }
    // InternalServerError (500)
    public static ActionResult<ApiResponse<T>> InternalServerErrorResponse<T>(
        this ControllerBase controller,
        string message,
        List<string>? errors = null)
    {
        return controller.StatusCode(
            StatusCodes.Status500InternalServerError,
            ApiResponse<T?>.CreateFailure(
                message: message,
                statusCode: StatusCodes.Status500InternalServerError,
                errors: errors
            )
        );
    }
}