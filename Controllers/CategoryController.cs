using CostMSWebAPI.DTOs;
using CostMSWebAPI.Models;
using CostMSWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostMSWebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("id/{id:long}")]
    public ActionResult<CategoryDto> GetById(long? id)
    {
        return Ok(_categoryService.GetById(id));
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpGet("user/{userId:long}")]
    public ActionResult<IEnumerable<CategoryDto>> GetByUserId(long userId)
    {
        return Ok(_categoryService.GetByUserId(userId));
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost("create")]
    public ActionResult<Category> Create(CategoryDto categoryDto)
    {
        return StatusCode(StatusCodes.Status201Created, _categoryService.Create(categoryDto));
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPut("update")]
    public ActionResult<Category> Update(CategoryDto categoryDto)
    {
        _categoryService.Update(categoryDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpDelete("delete")]
    public ActionResult<Category> Delete(CategoryDto categoryDto)
    {
        _categoryService.Delete(categoryDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpGet("all")]
    public ActionResult<IEnumerable<CategoryDto>> GetAll()
    {
        return Ok(_categoryService.GetAll());
    }
}