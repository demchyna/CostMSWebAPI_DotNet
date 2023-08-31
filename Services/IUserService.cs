using CostMSWebAPI.Models;

namespace CostMSWebAPI.Services;

public interface IUserService
{
    User? GetById(long? id);
    User? GetByUsername(string? username);
    User Create(User user);
    void Update(User user);
    void Delete(User user);
    IEnumerable<User> GetAll();
}
