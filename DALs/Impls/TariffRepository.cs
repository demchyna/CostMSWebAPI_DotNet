using CostMSWebAPI.Data;
using CostMSWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CostMSWebAPI.DALs.Impls;

public class TariffRepository : ITariffRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TariffRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Tariff? GetById(long? id)
    {
        return _dbContext.Tariffs
            .Where(tariff => tariff.Id == id)
            .Include(tariff => tariff.Unit)
            .Include(tariff => tariff.Category)
            .FirstOrDefault();
    }

    public IEnumerable<Tariff> GetByCategoryId(long? categoryId)
    {
        return _dbContext.Tariffs
            .Where(tariff => tariff.Category.Id == categoryId)
            .Include(tariff => tariff.Unit)
            .Include(tariff => tariff.Category)
            .ToList();
    }

    public Tariff Create(Tariff tariff, out int affectedRows)
    {
        _dbContext.Tariffs.Add(tariff);
        affectedRows = _dbContext.SaveChanges();
        return tariff;
    }

    public void Update(Tariff tariff, out int affectedRows)
    {
        _dbContext.Tariffs.Update(tariff);
        affectedRows = _dbContext.SaveChanges();
    }

    public void Delete(Tariff tariff, out int affectedRows)
    {

        _dbContext.Tariffs.Remove(tariff);
        affectedRows = _dbContext.SaveChanges();
    }

    public IEnumerable<Tariff> GetAll()
    {
        return _dbContext.Tariffs
            .Include(tariff => tariff.Unit)
            .Include(tariff => tariff.Category)
            .ToList();
    }
}