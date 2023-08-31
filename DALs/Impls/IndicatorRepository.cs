using CostMSWebAPI.Data;
using CostMSWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CostMSWebAPI.DALs.Impls;

public class IndicatorRepository : IIndicatorRepository
{
    private readonly ApplicationDbContext _dbContext;

    public IndicatorRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Indicator? GetById(long? id)
    {
        return _dbContext.Indicators
            .Where(indicator => indicator.Id == id)
            .Include(indicator => indicator.Previous)
            .Include(indicator => indicator.Meter)
            .Include(indicator => indicator.Tariff)
            .Include(indicator => indicator.Tariff.Unit)
            .FirstOrDefault();
    }

    public IEnumerable<Indicator> GetByMeterId(long? meterId)
    {
        return _dbContext.Indicators
            .Where(indicator => indicator.Meter.Id == meterId)
            .Include(indicator => indicator.Previous)
            .Include(indicator => indicator.Meter)
            .Include(indicator => indicator.Tariff)
            .Include(indicator => indicator.Tariff.Unit)
            .ToList();
    }

    public Indicator? GetLastAddedByMeterId(long? meterId)
    {
        return _dbContext.Indicators
            .FromSqlRaw("SELECT * FROM indicator WHERE meter_id = {0} ORDER BY id DESC", meterId ?? 0L)
            .FirstOrDefault();
    }

    public Indicator Create(Indicator indicator, out int affectedRows)
    {
        _dbContext.Indicators.Add(indicator);
        affectedRows = _dbContext.SaveChanges();
        return indicator;
    }

    public void Update(Indicator indicator, out int affectedRows)
    {
        _dbContext.Indicators.Update(indicator);
        affectedRows = _dbContext.SaveChanges();
    }

    public void Delete(Indicator indicator, out int affectedRows)
    {
        if (indicator.PreviousId != null) 
        {
            Indicator? nextIndicator = _dbContext.Indicators.FirstOrDefault(ind => ind.PreviousId == indicator.Id);
            if (nextIndicator != null)
            {
                nextIndicator.PreviousId = indicator.PreviousId;
            }
            indicator.PreviousId = null;
        }
        _dbContext.Indicators.Remove(indicator);
        affectedRows = _dbContext.SaveChanges();
    }

    public IEnumerable<Indicator> GetAll()
    {
        return _dbContext.Indicators.ToList();
    }
}