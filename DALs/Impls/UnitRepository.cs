using CostMSWebAPI.Data;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.DALs.Impls;

public class UnitRepository : IUnitRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UnitRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Unit? GetById(long? id)
    {
        return _dbContext.Units.Find(id);
    }

    public Unit Create(Unit unit, out int affectedRows)
    {
        _dbContext.Units.Add(unit);
        affectedRows = _dbContext.SaveChanges();
        return unit;
    }

    public void Update(Unit unit, out int affectedRows)
    {
        _dbContext.Units.Update(unit);
        affectedRows = _dbContext.SaveChanges();
    }

    public void Delete(Unit unit, out int affectedRows)
    {
        _dbContext.Units.Remove(unit);
        affectedRows = _dbContext.SaveChanges();
    }

    public IEnumerable<Unit> GetAll()
    {
        return _dbContext.Units.ToList();
    }
}