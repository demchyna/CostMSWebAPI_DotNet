using CostMSWebAPI.DTOs;
using CostMSWebAPI.Models;
using CostMSWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostMSWebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/indicator")]
public class IndicatorController : ControllerBase
{
    private readonly IIndicatorService _indicatorService;

    public IndicatorController(IIndicatorService indicatorService)
    {
        _indicatorService = indicatorService;
    }

    [HttpGet("id/{id:long}")]
    public ActionResult<IndicatorDto> GetById(long? id)
    {
        return Ok(_indicatorService.GetById(id));
    }
    
    [HttpGet("meter/{meterId:long}")]
    public ActionResult<IEnumerable<IndicatorDto>> GetByMeterId(long meterId)
    {
        return Ok(_indicatorService.GetByMeterId(meterId));
    }
    
    [HttpGet("last/meter/{meterId:long}")]
    public ActionResult<IndicatorDto> GetLastAddedByMeterId(long? meterId)
    {
        return Ok(_indicatorService.GetLastAddedByMeterId(meterId));
    }

    [HttpPost("create")]
    public ActionResult<Indicator> Create(IndicatorDto indicatorDto)
    {
        return StatusCode(StatusCodes.Status201Created, _indicatorService.Create(indicatorDto));
    }

    [HttpPut("update")]
    public ActionResult<Indicator> Update(IndicatorDto indicatorDto)
    {
        _indicatorService.Update(indicatorDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("delete")]
    public ActionResult<Indicator> Delete(IndicatorDto indicatorDto)
    {
        _indicatorService.Delete(indicatorDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpGet("all")]
    public ActionResult<IEnumerable<IndicatorDto>> GetAll()
    {
        return Ok(_indicatorService.GetAll());
    }
}