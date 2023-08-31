using System.Text;
using CostMSWebAPI.DALs;
using CostMSWebAPI.Exceptions;
using CostMSWebAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CostMSWebAPI.Services.Impls;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private int _affectedRows;

    public UserService(
        IUserRepository userRepository, 
        IRoleRepository roleRepository,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public User GetById(long? id)
    {
        if (id == null)
        {
           throw new ArgumentNullException(nameof(id), "The ID cannot be null.");
        }
        return _userRepository.GetById(id) ?? 
            throw new DataNotFoundException("The record with ID " + id + " not found.");
    }

    public User GetByUsername(string? username)
    {
        if (username == null)
        {
           throw new ArgumentNullException(nameof(username), "The Username cannot be null.");
        }
        return _userRepository.GetByUsername(username) ?? 
            throw new DataNotFoundException("The record with ID '" + username + "' not found.");
    }
    
    public User Create(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "The record cannot be null.");
        }
        if (_userRepository.GetByUsername(user.Username) != null) {
            throw new ConflictDataException("The user with the given username already exists.");
        }
        user.CreateDate = DateTime.UtcNow;
        Role? defaultRole = _roleRepository.GetByName("admin");
        if (defaultRole != null) 
        {
            user.Authorities.Add(defaultRole);
        }

        SaveIdentityUser(user, _userManager, _roleManager);

        User newUser = _userRepository.Create(user, out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not saved.");
        }
        newUser.Password = "[Protected]";

        return newUser;
    }

    public void Update(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "The record cannot be null.");
        }
        _userRepository.Update(user, out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The User entity is not updated.");
        }
    }

    public void Delete(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "The record cannot be null.");
        }
        _userRepository.Delete(user, out _affectedRows);
        if (_affectedRows == 0) {
            throw new IncorrectDataException("The record is not deleted.");
        }
    }

    public IEnumerable<User> GetAll()
    {
        return _userRepository.GetAll() ?? 
            throw new NoDataException("No records found.");
    }

    private static void SaveIdentityUser(User user, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (user.Password != null && user.Authorities.First().Name != null)
        {
            IdentityUser identityUser = new()
            {
                Email = user.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = user.Username
            };
            IdentityResult identityResult = userManager.CreateAsync(identityUser, user.Password).Result;       
            if (!identityResult.Succeeded)
            {
                StringBuilder errorsList = new();
                foreach (var error in identityResult.Errors)
                {
                    errorsList.Append(error.Description + " ");
                }
                throw new IncorrectDataException("The identity user is not saved: " + errorsList);
            }
            string authorityName = user.Authorities.First().Name ?? "";
            if (!roleManager.RoleExistsAsync(authorityName).Result)
            {
                throw new IncorrectDataException("The role " + authorityName + " not found.");
            }
            identityResult = userManager.AddToRoleAsync(identityUser, authorityName).Result;
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
}
