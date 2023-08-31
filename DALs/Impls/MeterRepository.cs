using CostMSWebAPI.Data;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.DALs.Impls;

public class MeterRepository : IMeterRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MeterRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Meter? GetById(long? id)
    {
        return _dbContext.Meters.Find(id);
    }

    public IEnumerable<Meter> GetByCategoryId(long? categoryId)
    {
        return _dbContext.Meters
            .Where(meter => meter.Category.Id == categoryId)
            .ToList();
    }

    public Meter Create(Meter meter, out int affectedRows)
    {
        _dbContext.Meters.Add(meter);
        affectedRows = _dbContext.SaveChanges();
        return meter;
    }

    public void Update(Meter meter, out int affectedRows)
    {
        _dbContext.Meters.Update(meter);
        affectedRows = _dbContext.SaveChanges();
    }

    public void Delete(Meter meter, out int affectedRows)
    {
        _dbContext.Meters.Attach(meter);
        meter.Indicators = _dbContext.Indicators.ToList();
        _dbContext.Meters.Remove(meter);
        affectedRows = _dbContext.SaveChanges();
    }

    public IEnumerable<Meter> GetAll()
    {
        return _dbContext.Meters.ToList();
    }
}