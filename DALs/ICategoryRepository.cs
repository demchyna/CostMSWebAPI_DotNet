using CostMSWebAPI.Models;

namespace CostMSWebAPI.DALs;

public interface ICategoryRepository
{
    Category? GetById(long? id);
    IEnumerable<Category> GetByUserId(long? userId);
    Category Create(Category category, out int affectedRows);
    void Update(Category category, out int affectedRows);
    void Delete(Category category, out int affectedRows);
    IEnumerable<Category> GetAll();
}