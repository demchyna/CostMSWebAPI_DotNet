using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CostMSWebAPI.Utils;

public class JwtSecurityTokenHelper
{
    public static JwtSecurityToken GetToken(IEnumerable<Claim> authClaims, IConfiguration configuration)
    {
        SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(configuration["JWT:Secret"] ?? ""));

        JwtSecurityToken jwtToken = new (
            issuer: configuration["JWT:ValidIssuer"],
            audience: configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512)
        );
        return jwtToken;
    }
}