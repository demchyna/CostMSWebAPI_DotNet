using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CostMSWebAPI.DTOs;
using CostMSWebAPI.Models;
using CostMSWebAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using CostMSWebAPI.Utils;

namespace CostMSWebAPI.Controllers;

[ApiController]
[Route("")]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserService _userService; 
    private readonly IRoleService _roleService; 

    public LoginController(IConfiguration configuration, 
                        UserManager<IdentityUser> userManager, 
                        IUserService userService, 
                        IRoleService roleService)
    {
        _configuration = configuration;
        _userManager = userManager;
        _userService = userService;
        _roleService = roleService;
    }

    [HttpPost("login")]
    public ActionResult<UserCredential> Login([FromBody] UserCredential userCredential)
    {
        IdentityUser? identityUser = _userManager.FindByNameAsync(userCredential.Username).Result;
        if (identityUser != null && _userManager.CheckPasswordAsync(identityUser, userCredential.Password).Result)
        {
            User? user = _userService.GetByUsername(identityUser.UserName ?? "");
            IList<Claim> authClaims = new List<Claim>
            {
                new("id", Convert.ToString(user?.Id ?? 0)),
                new("sub", identityUser.UserName ?? "")
            };
            IList<Role> userRoles = new List<Role>();
            foreach (string identityUserRole in _userManager.GetRolesAsync(identityUser).Result)
            {
                Role? role = _roleService.GetByName(identityUserRole.ToLower());
                if (role != null)
                {
                    userRoles.Add(role);
                }
            }
            authClaims.Add(new Claim("roles", 
                JsonSerializer.Serialize(userRoles, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = new LowerCaseNamingPolicy()
                }), 
                JsonClaimValueTypes.JsonArray));
            JwtSecurityToken jwtToken = JwtSecurityTokenHelper.GetToken(authClaims, _configuration);
            Response.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwtToken));
            return Ok();
        }
        return Unauthorized();
    }
}
