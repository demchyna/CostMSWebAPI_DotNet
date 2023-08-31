using CostMSWebAPI.DALs;
using CostMSWebAPI.Exceptions;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.Services.Impls;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;
    private int _affectedRows;

    public UnitService(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public Unit GetById(long? id)
    {
        if (id == null)
        {
           throw new ArgumentNullException(nameof(id), "The ID cannot be null.");
        }
        return _unitRepository.GetById(id) ?? 
            throw new DataNotFoundException("The record with ID " + id + " not found.");
    }
    
    public Unit Create(Unit unit)
    {
        if (unit == null)
        {
            throw new ArgumentNullException(nameof(unit), "The record cannot be null.");
        }
        Unit newUnit = _unitRepository.Create(unit, out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not saved.");
        }
        return newUnit;
    }

    public void Update(Unit unit)
    {
        if (unit == null)
        {
            throw new ArgumentNullException(nameof(unit), "The record cannot be null.");
        }
        _unitRepository.Update(unit, out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The Unit entity is not updated.");
        }
    }

    public void Delete(Unit unit)
    {
        if (unit == null)
        {
            throw new ArgumentNullException(nameof(unit), "The record cannot be null.");
        }
        _unitRepository.Delete(unit, out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not deleted.");
        }
    }

    public IEnumerable<Unit> GetAll()
    {
        return _unitRepository.GetAll() ?? 
            throw new NoDataException("No records found.");
    }
}
