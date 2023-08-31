using CostMSWebAPI.Models;
using CostMSWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostMSWebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/unit")]
public class UnitController : ControllerBase
{
    private readonly IUnitService _unitService;

    public UnitController(IUnitService unitService)
    {
        _unitService = unitService;
    }

    [HttpGet("id/{id}")]
    public ActionResult<Unit> GetById(long? id) 
    {
        return Ok(_unitService.GetById(id));
    }

    [HttpPost("create")]
    public ActionResult<Unit> Create(Unit unit)
    {
        return StatusCode(StatusCodes.Status201Created, _unitService.Create(unit));
    }

    [HttpPut("update")]
    public ActionResult<Unit> Update(Unit unit)
    {
        _unitService.Update(unit);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("delete")]
    public ActionResult<Unit> Delete(Unit unit)
    {
        _unitService.Delete(unit);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpGet("all")]
    public ActionResult<IEnumerable<Unit>> GetAll()
    {
        return Ok(_unitService.GetAll());
    }

    // --- Dapper approach ---

    // private readonly MySqlConnection _connection;

    // public UnitController(MySqlConnection connection)
    // {
    //     _connection = connection;
    // }

    // [HttpGet("{id}")]
    // public Unit Get(int id) {
    //     var record = _connection.QuerySingle<dynamic>("SELECT * FROM unit WHERE id=" + id);
    //     return new Unit()
    //     {
    //         Id = record.id,
    //         Name = record.name,
    //         Description = record.description
    //     };
    // }

    // [HttpGet("All")]
    // public IEnumerable<Unit> GetAll()
    // {
    //     return _connection
    //         .Query<dynamic>("SELECT * FROM unit")
    //         .Select(record => 
    //             new Unit()
    //             {
    //                 Id = record.id,
    //                 Name = record.name,
    //                 Description = record.description
    //             })
    //         .ToList();
    // }
}
