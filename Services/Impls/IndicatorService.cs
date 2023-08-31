using AutoMapper;
using CostMSWebAPI.DALs;
using CostMSWebAPI.DTOs;
using CostMSWebAPI.Exceptions;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.Services.Impls;

public class IndicatorService : IIndicatorService
{
    private readonly IIndicatorRepository _indicatorRepository;
    private int _affectedRows;

    public IndicatorService(IIndicatorRepository indicatorRepository)
    {
        _indicatorRepository = indicatorRepository;
    }

    public IndicatorDto GetById(long? id)
    {
        if (id == null)
        {
           throw new ArgumentNullException(nameof(id), "The ID cannot be null.");
        }
        
        Indicator indicator = _indicatorRepository.GetById(id) ?? 
            throw new DataNotFoundException("The record with ID " + id + " not found.");

        IndicatorDto indicatorDto = GetMapper().Map<IndicatorDto>(indicator);

        return indicatorDto;
    }

    public IEnumerable<IndicatorDto> GetByMeterId(long? meterId)
    {
        if (meterId == null)
        {
           throw new ArgumentNullException(nameof(meterId), "The meter ID cannot be null.");
        }
        IEnumerable<Indicator> indicatorList = _indicatorRepository.GetByMeterId(meterId) ??
            throw new NoDataException("No records found.");
        return indicatorList.Select(indicator => GetMapper().Map<IndicatorDto>(indicator)).ToList();
    }

    public IndicatorDto? GetLastAddedByMeterId(long? meterId)
    {
        if (meterId == null)
        {
            throw new ArgumentNullException(nameof(meterId), "The meter ID cannot be null.");
        }

        Indicator? indicator = _indicatorRepository.GetLastAddedByMeterId(meterId);
        return indicator != null ? GetMapper().Map<IndicatorDto>(indicator) : null;
    }

    public Indicator Create(IndicatorDto indicatorDto)
    {
        if (indicatorDto == null)
        {
            throw new ArgumentNullException(nameof(indicatorDto), "The record cannot be null.");
        }
        Indicator newIndicator = _indicatorRepository.Create(GetMapper().Map<Indicator>(indicatorDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not saved.");
        }
        return newIndicator;
    }

    public void Update(IndicatorDto indicatorDto)
    {
        if (indicatorDto == null)
        {
            throw new ArgumentNullException(nameof(indicatorDto), "The record cannot be null.");
        }
        _indicatorRepository.Update(GetMapper().Map<Indicator>(indicatorDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The Indicator entity is not updated.");
        }
    }

    public void Delete(IndicatorDto indicatorDto)
    {
        if (indicatorDto == null)
        {
            throw new ArgumentNullException(nameof(indicatorDto), "The record cannot be null.");
        }
        _indicatorRepository.Delete(GetMapper().Map<Indicator>(indicatorDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not deleted.");
        }
    }

    public IEnumerable<IndicatorDto> GetAll()
    {
        IEnumerable<Indicator> indicatorList = _indicatorRepository.GetAll() ??
            throw new NoDataException("No records found.");
        return indicatorList.Select(indicator => GetMapper().Map<IndicatorDto>(indicator)).ToList();
    }
    
    private static Mapper GetMapper()
    {
        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<IndicatorDto, Indicator>()
                    .ForMember(dest => dest.Previous, opt => opt.MapFrom<Indicator>(srs => null));
                cfg.CreateMap<Indicator, IndicatorDto>()
                    .ForMember(dest => dest.Previous, opt => opt.MapFrom(srs => srs.Previous.Current))
                    .ForMember(dest => dest.TariffRate, opt => opt.MapFrom(srs => srs.Tariff.Rate))
                    .ForMember(dest => dest.TariffCurrency, opt => opt.MapFrom(srs => srs.Tariff.Currency))
                    .ForMember(dest => dest.UnitName, opt => opt.MapFrom(srs => srs.Tariff.Unit.Name))
                    .ForMember(dest => dest.Price,
                        opt => opt.MapFrom(srs => CalculatePrice(srs)));
            });
        return new Mapper(config);
    }

    private static decimal CalculatePrice(Indicator indicator)
    {
        if (indicator.Previous == null)
        {
            return indicator.Tariff.Rate * (indicator.Current - 0);
        }
        return indicator.Tariff.Rate * (indicator.Current - indicator.Previous.Current);
    }
}