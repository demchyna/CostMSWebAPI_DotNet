using CostMSWebAPI.Data;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.DALs.Impls;

public class FundsRepository : IFundsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FundsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Funds? GetById(long? id)
    {
        return _dbContext.Funds.Find(id);
    }

    public IEnumerable<Funds> GetByUserId(long? userId)
    {
        return _dbContext.Funds
            .Where(funds => funds.User.Id == userId)
            .ToList();
    }

    public Funds Create(Funds funds, out int affectedRows)
    {
        _dbContext.Funds.Add(funds);
        affectedRows = _dbContext.SaveChanges();
        return funds;
    }

    public void Update(Funds funds, out int affectedRows)
    {
        _dbContext.Funds.Update(funds);
        affectedRows = _dbContext.SaveChanges();
    }

    public void Delete(Funds funds, out int affectedRows)
    {

        _dbContext.Funds.Remove(funds);
        affectedRows = _dbContext.SaveChanges();
    }

    public IEnumerable<Funds> GetAll()
    {
        return _dbContext.Funds.ToList();
    }
}
