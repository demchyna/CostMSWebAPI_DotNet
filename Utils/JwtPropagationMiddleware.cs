using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.Utils;

public class JwtPropagationMiddleware
{
    private readonly RequestDelegate _next;

    public JwtPropagationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IConfiguration configuration)
    {
        if (httpContext.User.Identity is { IsAuthenticated: true })
        {
            string? authHeader = httpContext.Request.Headers.Authorization;
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            if (jwtSecurityTokenHandler.CanReadToken(authHeader?[7..]))
            {
                JwtSecurityToken securityToken = jwtSecurityTokenHandler.ReadJwtToken(authHeader?[7..]);
                List<Claim> securityTokenClaims = securityToken.Claims.ToList();

                IList<Role?> userRoles = securityTokenClaims
                    .FindAll(claim => claim.Type == "roles")
                    .Select(claim => JsonSerializer.Deserialize<Role>(claim.Value, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    })).ToList();
                
                securityToken = JwtSecurityTokenHelper.GetToken(new List<Claim>()
                {
                    securityTokenClaims.Find(claim => claim.Type == "id")!,
                    securityTokenClaims.Find(claim => claim.Type == "sub")!,
                    
                    new("roles", 
                        JsonSerializer.Serialize(userRoles, new JsonSerializerOptions 
                        {
                            PropertyNamingPolicy = new LowerCaseNamingPolicy()
                        }), 
                        JsonClaimValueTypes.JsonArray)
                    
                }, configuration);            
                httpContext.Response.Headers.Add("Authorization", 
                    "Bearer " + new JwtSecurityTokenHandler().WriteToken(securityToken));
            }
        }
        await _next(httpContext);
    }
}

public static class JwtPropagationMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtPropagation(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtPropagationMiddleware>();
    }
}