using AutoMapper;
using CostMSWebAPI.DALs;
using CostMSWebAPI.DTOs;
using CostMSWebAPI.Exceptions;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.Services.Impls;

public class TariffService : ITariffService
{
    private readonly ITariffRepository _tariffRepository;
    private int _affectedRows;

    public TariffService(ITariffRepository tariffRepository)
    {
        _tariffRepository = tariffRepository;
    }

    public TariffDto GetById(long? id)
    {
        if (id == null)
        {
           throw new ArgumentNullException(nameof(id), "The ID cannot be null.");
        }
        Tariff tariff = _tariffRepository.GetById(id) ?? 
            throw new DataNotFoundException("The record with ID " + id + " not found.");
        return GetMapper().Map<TariffDto>(tariff);
    }

    public IEnumerable<TariffDto>  GetByCategoryId(long? categoryId)
    {
        if (categoryId == null)
        {
           throw new ArgumentNullException(nameof(categoryId), "The category ID cannot be null.");
        }
        IEnumerable<Tariff> tariffList = _tariffRepository.GetByCategoryId(categoryId) ??
            throw new NoDataException("No records found.");
        return tariffList.Select(tariff => GetMapper().Map<TariffDto>(tariff)).ToList();
    }
    
    public Tariff Create(TariffDto tariffDto)
    {
        if (tariffDto == null)
        {
            throw new ArgumentNullException(nameof(tariffDto), "The record cannot be null.");
        }
        Tariff newTariff = _tariffRepository.Create(GetMapper().Map<Tariff>(tariffDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not saved.");
        }
        return newTariff;
    }

    public void Update(TariffDto tariffDto)
    {
        if (tariffDto == null)
        {
            throw new ArgumentNullException(nameof(tariffDto), "The record cannot be null.");
        }
        _tariffRepository.Update(GetMapper().Map<Tariff>(tariffDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The Tariff entity is not updated.");
        }
    }

    public void Delete(TariffDto tariffDto)
    {
        if (tariffDto == null)
        {
            throw new ArgumentNullException(nameof(tariffDto), "The record cannot be null.");
        }
        _tariffRepository.Delete(GetMapper().Map<Tariff>(tariffDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not deleted.");
        }
    }

    public IEnumerable<TariffDto> GetAll()
    {
        IEnumerable<Tariff> tariffList = _tariffRepository.GetAll() ??
            throw new NoDataException("No records found.");
        return tariffList.Select(tariff => GetMapper().Map<TariffDto>(tariff)).ToList();
    }
    
    private static Mapper GetMapper()
    {
        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<TariffDto, Tariff>();
                cfg.CreateMap<Tariff, TariffDto>()
                    .ForMember(dest => dest.UnitName, opt => opt.MapFrom(srs => srs.Unit.Name));
            });
        return new Mapper(config);
    }
}