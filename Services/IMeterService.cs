using CostMSWebAPI.DTOs;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.Services;

public interface IMeterService
{
    MeterDto? GetById(long? id);
    IEnumerable<MeterDto> GetByCategoryId(long? categoryId);
    Meter Create(MeterDto meterDto);
    void Update(MeterDto meterDto);
    void Delete(MeterDto meterDto);
    IEnumerable<MeterDto> GetAll();
}