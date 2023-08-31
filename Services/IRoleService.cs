using CostMSWebAPI.Models;

namespace CostMSWebAPI.Services;

public interface IRoleService
{
    Role? GetById(long? id);
    Role? GetByName(string? name);
    Role Create(Role role);
    void Update(Role role);
    void Delete(Role role);
    IEnumerable<Role> GetAll();
}
