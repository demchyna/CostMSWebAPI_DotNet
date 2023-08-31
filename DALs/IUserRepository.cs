using CostMSWebAPI.Models;

namespace CostMSWebAPI.DALs;

public interface IUserRepository
{
    User? GetById(long? id);
    User? GetByUsername(string? username);
    User Create(User user, out int affectedRows);
    void Update(User user, out int affectedRows);
    void Delete(User user, out int affectedRows);
    IEnumerable<User> GetAll();
}
