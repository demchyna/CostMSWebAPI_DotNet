using CostMSWebAPI.Data;
using CostMSWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CostMSWebAPI.DALs.Impls;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Category? GetById(long? id)
    {
        return _dbContext.Categories.Find(id);
    }

    public IEnumerable<Category> GetByUserId(long? userId)
    {
        return _dbContext.Categories
            .Where(category => category.User.Id == userId)
            .ToList();
    }

    public Category Create(Category category, out int affectedRows)
    {
        _dbContext.Categories.Add(category);
        affectedRows = _dbContext.SaveChanges();
        return category;
    }

    public void Update(Category category, out int affectedRows)
    {
        _dbContext.Categories.Update(category);
        affectedRows = _dbContext.SaveChanges();
    }

    public void Delete(Category category, out int affectedRows)
    {
        _dbContext.Categories.Attach(category);
        category.Meters = _dbContext.Meters.ToList();
        foreach (Meter meter in category.Meters)
        {
            meter.Indicators = _dbContext.Indicators.ToList();
        }
        category.Tariffs = _dbContext.Tariffs.ToList();
        _dbContext.Categories.Remove(category);
        affectedRows = _dbContext.SaveChanges();
    }

    public IEnumerable<Category> GetAll()
    {
        return _dbContext.Categories.ToList();
    }
}