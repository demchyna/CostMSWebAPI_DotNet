using CostMSWebAPI.DTOs;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.Services;

public interface IFundsService
{
    FundsDto? GetById(long? id);
    IEnumerable<FundsDto> GetByUserId(long? userId);
    Funds Create(FundsDto fundsDto);
    void Update(FundsDto fundsDto);
    void Delete(FundsDto fundsDto);
    IEnumerable<FundsDto> GetAll();
}
