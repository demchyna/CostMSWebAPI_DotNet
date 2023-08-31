using System.Text;
using CostMSWebAPI.DALs;
using CostMSWebAPI.Exceptions;
using CostMSWebAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CostMSWebAPI.Services.Impls;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly RoleManager<IdentityRole> _roleManager;
    private int _affectedRows;

    public RoleService(IRoleRepository roleRepository, RoleManager<IdentityRole> roleManager)
    {
        _roleRepository = roleRepository;
        _roleManager = roleManager;
    }

    public Role GetById(long? id)
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id), "The ID cannot be null.");
        }
        return _roleRepository.GetById(id) ??
            throw new DataNotFoundException("The record with ID " + id + " not found.");
    }

    public Role GetByName(string? name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name), "The Rolename cannot be null.");
        }
        return _roleRepository.GetByName(name) ??
            throw new DataNotFoundException("The record with ID '" + name + "' not found.");
    }

    public Role Create(Role role)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role), "The record cannot be null.");
        }
        if (_roleRepository.GetByName(role.Name) != null)
        {
            throw new ConflictDataException("The role with the given rolename already exists.");
        }

        SaveIdentityRole(role, _roleManager);

        Role newRole = _roleRepository.Create(role, out _affectedRows);
        if (_affectedRows == 0)
        {
            throw new IncorrectDataException("The record is not saved.");
        }
        return newRole;
    }

    public void Update(Role role)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role), "The record cannot be null.");
        }
        _roleRepository.Update(role, out _affectedRows);
        if (_affectedRows == 0)
        {
            throw new IncorrectDataException("The Role entity is not updated.");
        }
    }

    public void Delete(Role role)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role), "The record cannot be null.");
        }
        _roleRepository.Delete(role, out _affectedRows);
        if (_affectedRows == 0)
        {
            throw new IncorrectDataException("The record is not deleted.");
        }
    }

    public IEnumerable<Role> GetAll()
    {
        return _roleRepository.GetAll() ??
            throw new NoDataException("No records found.");
    }

    private static void SaveIdentityRole(Role role, RoleManager<IdentityRole> roleManager)
    {
        if (role.Name == null)
        {
            throw new IncorrectDataException("The name of role cannot be null.");
        }
        string roleName = role.Name;
        if (roleManager.RoleExistsAsync(roleName).Result)
        {
            throw new ConflictDataException("The role with the given name already exists.");
        }
        IdentityResult identityResult = roleManager.CreateAsync(new IdentityRole(roleName)).Result;
        if (!identityResult.Succeeded)
        {
            StringBuilder errorsList = new();
            foreach (var error in identityResult.Errors)
            {
                errorsList.Append(error.Description + " ");
            }
            throw new IncorrectDataException("The identity role is not saved: " + errorsList);
        }
    }
}
