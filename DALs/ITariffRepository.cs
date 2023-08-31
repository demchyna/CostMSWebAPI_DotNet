using CostMSWebAPI.Models;

namespace CostMSWebAPI.DALs;

public interface ITariffRepository
{
    Tariff? GetById(long? id);
    IEnumerable<Tariff> GetByCategoryId(long? categoryId);
    Tariff Create(Tariff tariff, out int affectedRows);
    void Update(Tariff tariff, out int affectedRows);
    void Delete(Tariff tariff, out int affectedRows);
    IEnumerable<Tariff> GetAll();
}