using CostMSWebAPI.Models;

namespace CostMSWebAPI.DALs;

public interface IMeterRepository
{
    Meter? GetById(long? id);
    IEnumerable<Meter> GetByCategoryId(long? categoryId);
    Meter Create(Meter meter, out int affectedRows);
    void Update(Meter meter, out int affectedRows);
    void Delete(Meter meter, out int affectedRows);
    IEnumerable<Meter> GetAll();
}