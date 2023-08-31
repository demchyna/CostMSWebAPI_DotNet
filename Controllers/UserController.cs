using CostMSWebAPI.Models;
using CostMSWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostMSWebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("id/{id}")]
    public ActionResult<User> GetById(long? id)
    {
        User? user = _userService.GetById(id);
        if (user != null)
        {
            user.Password = "";
        }
        return Ok(user);
    }
    
    [HttpPut("update")]
    public ActionResult<User> Update(User user)
    {
        _userService.Update(user);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("delete")]
    public ActionResult<User> Delete(User user)
    {
        _userService.Delete(user);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpGet("all")]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        return Ok(_userService.GetAll());
    }
}