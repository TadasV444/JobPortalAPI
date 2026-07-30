using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.Api.Extentions;

public static class UserPrincipalExtensions
{
    public static int GetUserId(this ControllerBase controller)
        => int.Parse(controller.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}