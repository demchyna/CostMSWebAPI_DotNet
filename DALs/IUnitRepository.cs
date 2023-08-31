using CostMSWebAPI.Models;

namespace CostMSWebAPI.DALs;

public interface IUnitRepository
{
    Unit? GetById(long? id);
    Unit Create(Unit unit, out int affectedRows);
    void Update(Unit unit, out int affectedRows);
    void Delete(Unit unit, out int affectedRows);
    IEnumerable<Unit> GetAll();
}
