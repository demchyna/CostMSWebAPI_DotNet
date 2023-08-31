using CostMSWebAPI.DTOs;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.Services;

public interface ICategoryService
{
    CategoryDto? GetById(long? id);
    IEnumerable<CategoryDto> GetByUserId(long? userId);
    Category Create(CategoryDto categoryDto);
    void Update(CategoryDto categoryDto);
    void Delete(CategoryDto categoryDto);
    IEnumerable<CategoryDto> GetAll();
}