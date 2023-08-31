using AutoMapper;
using CostMSWebAPI.DALs;
using CostMSWebAPI.DTOs;
using CostMSWebAPI.Exceptions;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.Services.Impls;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private int _affectedRows;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public CategoryDto GetById(long? id)
    {
        if (id == null)
        {
           throw new ArgumentNullException(nameof(id), "The ID cannot be null.");
        }
        
        Category category = _categoryRepository.GetById(id) ?? 
            throw new DataNotFoundException("The record with ID " + id + " not found.");

        return GetMapper().Map<CategoryDto>(category);
    }

    public IEnumerable<CategoryDto> GetByUserId(long? userId)
    {
        if (userId == null)
        {
           throw new ArgumentNullException(nameof(userId), "The user ID cannot be null.");
        }
        IEnumerable<Category> categoryList = _categoryRepository.GetByUserId(userId) ??
            throw new NoDataException("No records found.");
        return categoryList.Select(category => GetMapper().Map<CategoryDto>(category)).ToList();
    }
    
    public Category Create(CategoryDto categoryDto)
    {
        if (categoryDto == null)
        {
            throw new ArgumentNullException(nameof(categoryDto), "The record cannot be null.");
        }
        Category newCategory = _categoryRepository.Create(GetMapper().Map<Category>(categoryDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not saved.");
        }
        return newCategory;
    }

    public void Update(CategoryDto categoryDto)
    {
        if (categoryDto == null)
        {
            throw new ArgumentNullException(nameof(categoryDto), "The record cannot be null.");
        }
        _categoryRepository.Update(GetMapper().Map<Category>(categoryDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The Category entity is not updated.");
        }
    }

    public void Delete(CategoryDto categoryDto)
    {
        if (categoryDto == null)
        {
            throw new ArgumentNullException(nameof(categoryDto), "The record cannot be null.");
        }
        _categoryRepository.Delete(GetMapper().Map<Category>(categoryDto), out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not deleted.");
        }
    }

    public IEnumerable<CategoryDto> GetAll()
    {
        IEnumerable<Category> categoryList = _categoryRepository.GetAll() ??
            throw new NoDataException("No records found.");
        return categoryList.Select(category => GetMapper().Map<CategoryDto>(category)).ToList();
    }
    
    private static Mapper GetMapper()
    {
        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<CategoryDto, Category>();
                cfg.CreateMap<Category, CategoryDto>();
            });
        return new Mapper(config);
    }
}