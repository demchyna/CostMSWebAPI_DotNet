using CostMSWebAPI.Models;

namespace CostMSWebAPI.DALs;

public interface IFundsRepository
{
    Funds? GetById(long? id);
    IEnumerable<Funds> GetByUserId(long? userId);
    Funds Create(Funds funds, out int affectedRows);
    void Update(Funds funds, out int affectedRows);
    void Delete(Funds funds, out int affectedRows);
    IEnumerable<Funds> GetAll();
}
