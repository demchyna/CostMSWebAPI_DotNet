using CostMSWebAPI.DTOs;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.Services;

public interface IIndicatorService
{
    IndicatorDto? GetById(long? id);
    IEnumerable<IndicatorDto> GetByMeterId(long? meterId);
    IndicatorDto? GetLastAddedByMeterId(long? meterId);
    Indicator Create(IndicatorDto indicatorDto);
    void Update(IndicatorDto indicatorDto);
    void Delete(IndicatorDto indicatorDto);
    IEnumerable<IndicatorDto> GetAll();
}