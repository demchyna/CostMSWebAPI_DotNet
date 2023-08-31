using CostMSWebAPI.DTOs;
using CostMSWebAPI.Models;
using CostMSWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostMSWebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/meter")]
public class MeterController : ControllerBase
{
    private readonly IMeterService _meterService;

    public MeterController(IMeterService meterService)
    {
        _meterService = meterService;
    }

    [HttpGet("id/{id:long}")]
    public ActionResult<MeterDto> GetById(long? id)
    {
        return Ok(_meterService.GetById(id));
    }
    
    [HttpGet("category/{categoryId:long}")]
    public ActionResult<IEnumerable<MeterDto>> GetByCategoryId(long categoryId)
    {
        return Ok(_meterService.GetByCategoryId(categoryId));
    }

    [HttpPost("create")]
    public ActionResult<Meter> Create(MeterDto meterDto)
    {
        return StatusCode(StatusCodes.Status201Created, _meterService.Create(meterDto));
    }

    [HttpPut("update")]
    public ActionResult<Meter> Update(MeterDto meterDto)
    {
        _meterService.Update(meterDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("delete")]
    public ActionResult<Meter> Delete(MeterDto meterDto)
    {
        _meterService.Delete(meterDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpGet("all")]
    public ActionResult<IEnumerable<MeterDto>> GetAll()
    {
        return Ok(_meterService.GetAll());
    }
}