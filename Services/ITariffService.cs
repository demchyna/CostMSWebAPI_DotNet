using CostMSWebAPI.DTOs;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.Services;

public interface ITariffService
{
    TariffDto? GetById(long? id);
    IEnumerable<TariffDto> GetByCategoryId(long? categoryId);
    Tariff Create(TariffDto tariffDto);
    void Update(TariffDto tariffDto);
    void Delete(TariffDto tariffDto);
    IEnumerable<TariffDto> GetAll();
}