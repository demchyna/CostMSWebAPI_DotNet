using CostMSWebAPI.Models;

namespace CostMSWebAPI.DALs;

public interface IIndicatorRepository
{
    Indicator? GetById(long? id);
    IEnumerable<Indicator> GetByMeterId(long? meterId);
    Indicator? GetLastAddedByMeterId(long? meterId);
    Indicator Create(Indicator indicator, out int affectedRows);
    void Update(Indicator indicator, out int affectedRows);
    void Delete(Indicator indicator, out int affectedRows);
    IEnumerable<Indicator> GetAll();
}