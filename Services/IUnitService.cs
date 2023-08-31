using CostMSWebAPI.Models;

namespace CostMSWebAPI.Services;

public interface IUnitService
{
    Unit? GetById(long? id);
    Unit Create(Unit unit);
    void Update(Unit unit);
    void Delete(Unit unit);
    IEnumerable<Unit> GetAll();
}
