using CostMSWebAPI.DTOs;
using CostMSWebAPI.Models;
using CostMSWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostMSWebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/funds")]
public class FundsController : ControllerBase
{
    private readonly IFundsService _fundsService;

    public FundsController(IFundsService fundsService)
    {
        _fundsService = fundsService;
    }

    [HttpGet("id/{id:long}")]
    public ActionResult<FundsDto> GetById(long? id)
    {
        return Ok(_fundsService.GetById(id));
    }
    
    [HttpGet("user/{userId:long}")]
    public ActionResult<IEnumerable<FundsDto>> GetByUserId(long userId)
    {
        return Ok(_fundsService.GetByUserId(userId));
    }

    [HttpPost("create")]
    public ActionResult<Funds> Create(FundsDto fundsDto)
    {
        return StatusCode(StatusCodes.Status201Created, _fundsService.Create(fundsDto));
    }

    [HttpPut("update")]
    public ActionResult<Funds> Update(FundsDto fundsDto)
    {
        _fundsService.Update(fundsDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("delete")]
    public ActionResult<Funds> Delete(FundsDto fundsDto)
    {
        _fundsService.Delete(fundsDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpGet("all")]
    public ActionResult<IEnumerable<FundsDto>> GetAll()
    {
        return Ok(_fundsService.GetAll());
    }
}
