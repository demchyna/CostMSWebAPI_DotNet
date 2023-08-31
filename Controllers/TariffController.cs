using CostMSWebAPI.DTOs;
using CostMSWebAPI.Models;
using CostMSWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostMSWebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/tariff")]
public class TariffController : ControllerBase
{
    private readonly ITariffService _tariffService;

    public TariffController(ITariffService tariffService)
    {
        _tariffService = tariffService;
    }

    [HttpGet("id/{id:long}")]
    public ActionResult<TariffDto> GetById(long? id)
    {
        return Ok(_tariffService.GetById(id));
    }
    
    [HttpGet("category/{categoryId:long}")]
    public ActionResult<IEnumerable<TariffDto>> GetByCategoryId(long categoryId)
    {
        return Ok(_tariffService.GetByCategoryId(categoryId));
    }

    [HttpPost("create")]
    public ActionResult<Tariff> Create(TariffDto tariffDto)
    {
        return StatusCode(StatusCodes.Status201Created, _tariffService.Create(tariffDto));
    }

    [HttpPut("update")]
    public ActionResult<Tariff> Update(TariffDto tariffDto)
    {
        _tariffService.Update(tariffDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("delete")]
    public ActionResult<Tariff> Delete(TariffDto tariffDto)
    {
        _tariffService.Delete(tariffDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpGet("all")]
    public ActionResult<IEnumerable<TariffDto>> GetAll()
    {
        return Ok(_tariffService.GetAll());
    }
}