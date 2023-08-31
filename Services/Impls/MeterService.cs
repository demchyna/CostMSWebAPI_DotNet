using AutoMapper;
using CostMSWebAPI.DALs;
using CostMSWebAPI.DTOs;
using CostMSWebAPI.Exceptions;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.Services.Impls;

public class MeterService : IMeterService
{
    private readonly IMeterRepository _meterRepository;
    private int _affectedRows;

    public MeterService(IMeterRepository meterRepository)
    {
        _meterRepository = meterRepository;
    }

    public MeterDto GetById(long? id)
    {
        if (id == null)
        {
           throw new ArgumentNullException(nameof(id), "The ID cannot be null.");
        }
        
        Meter meter = _meterRepository.GetById(id) ?? 
            throw new DataNotFoundException("The record with ID " + id + " not found.");

        return GetMapper().Map<MeterDto>(meter);
    }

    public IEnumerable<MeterDto> GetByCategoryId(long? categoryId)
    {
        if (categoryId == null)
        {
           throw new ArgumentNullException(nameof(categoryId), "The category ID cannot be null.");
        }
        IEnumerable<Meter> meterList = _meterRepository.GetByCategoryId(categoryId) ??
            throw new NoDataException("No records found.");
        return meterList.Select(meter => GetMapper().Map<MeterDto>(meter)).ToList();
    }
    
    public Meter Create(MeterDto meterDto)
    {
        if (meterDto == null)
        {
            throw new ArgumentNullException(nameof(meterDto), "The record cannot be null.");
        }
        Meter newMeter = _meterRepository.Create(GetMapper().Map<Meter>(meterDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not saved.");
        }
        return newMeter;
    }

    public void Update(MeterDto meterDto)
    {
        if (meterDto == null)
        {
            throw new ArgumentNullException(nameof(meterDto), "The record cannot be null.");
        }
        _meterRepository.Update(GetMapper().Map<Meter>(meterDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The Meter entity is not updated.");
        }
    }

    public void Delete(MeterDto meterDto)
    {
        if (meterDto == null)
        {
            throw new ArgumentNullException(nameof(meterDto), "The record cannot be null.");
        }
        _meterRepository.Delete(GetMapper().Map<Meter>(meterDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not deleted.");
        }
    }

    public IEnumerable<MeterDto> GetAll()
    {
        IEnumerable<Meter> meterList = _meterRepository.GetAll() ??
            throw new NoDataException("No records found.");
        return meterList.Select(meter => GetMapper().Map<MeterDto>(meter)).ToList();
    }
    
    private static Mapper GetMapper()
    {
        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<MeterDto, Meter>();
                cfg.CreateMap<Meter, MeterDto>();
            });
        return new Mapper(config);
    }
}