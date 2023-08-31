using CostMSWebAPI.Models;
using CostMSWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostMSWebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/role")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("id/{id}")]
    public ActionResult<Role> GetById(long? id) 
    {
        return Ok(_roleService.GetById(id));
    }

    [HttpPost("create")]
    public ActionResult<Role> Create(Role role)
    {
        return StatusCode(StatusCodes.Status201Created, _roleService.Create(role));
    }

    [HttpPut("update")]
    public ActionResult<Role> Update(Role role)
    {
        _roleService.Update(role);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("delete")]
    public ActionResult<Role> Delete(Role role)
    {
        _roleService.Delete(role);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpGet("all")]
    public ActionResult<IEnumerable<Role>> GetAll()
    {
        return Ok(_roleService.GetAll());
    }
}
