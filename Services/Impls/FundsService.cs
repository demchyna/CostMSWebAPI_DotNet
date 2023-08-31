using AutoMapper;
using CostMSWebAPI.DALs;
using CostMSWebAPI.DTOs;
using CostMSWebAPI.Exceptions;
using CostMSWebAPI.Models;
using Microsoft.OpenApi.Extensions;

namespace CostMSWebAPI.Services.Impls;

public class FundsService : IFundsService
{
    private readonly IFundsRepository _fundsRepository;
    private int _affectedRows;

    public FundsService(IFundsRepository fundsRepository)
    {
        _fundsRepository = fundsRepository;
    }

    public FundsDto GetById(long? id)
    {
        if (id == null)
        {
           throw new ArgumentNullException(nameof(id), "The ID cannot be null.");
        }
        
        Funds funds = _fundsRepository.GetById(id) ?? 
            throw new DataNotFoundException("The record with ID " + id + " not found.");

        return GetMapper().Map<FundsDto>(funds);
    }

    public IEnumerable<FundsDto> GetByUserId(long? userId)
    {
        if (userId == null)
        {
           throw new ArgumentNullException(nameof(userId), "The user ID cannot be null.");
        }
        IEnumerable<Funds> fundsList = _fundsRepository.GetByUserId(userId) ??
            throw new NoDataException("No records found.");
        return fundsList.Select(funds => GetMapper().Map<FundsDto>(funds)).ToList();
    }
    
    public Funds Create(FundsDto fundsDto)
    {
        if (fundsDto == null)
        {
            throw new ArgumentNullException(nameof(fundsDto), "The record cannot be null.");
        }
        Funds newFunds = _fundsRepository.Create(GetMapper().Map<Funds>(fundsDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not saved.");
        }
        return newFunds;
    }

    public void Update(FundsDto fundsDto)
    {
        if (fundsDto == null)
        {
            throw new ArgumentNullException(nameof(fundsDto), "The record cannot be null.");
        }
        _fundsRepository.Update(GetMapper().Map<Funds>(fundsDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The Funds entity is not updated.");
        }
    }

    public void Delete(FundsDto fundsDto)
    {
        if (fundsDto == null)
        {
            throw new ArgumentNullException(nameof(fundsDto), "The record cannot be null.");
        }
        _fundsRepository.Delete(GetMapper().Map<Funds>(fundsDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not deleted.");
        }
    }

    public IEnumerable<FundsDto> GetAll()
    {
        IEnumerable<Funds> fundsList = _fundsRepository.GetAll() ??
            throw new NoDataException("No records found.");
        return fundsList.Select(funds => GetMapper().Map<FundsDto>(funds)).ToList();
    }
    
    private static Mapper GetMapper()
    {
        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<FundsDto, Funds>()
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(srs => Enum.Parse<FundsType>(srs.Type ?? "")));
                cfg.CreateMap<Funds, FundsDto>()
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(srs => srs.Type.GetDisplayName()));
            });
        return new Mapper(config);
    }
}
