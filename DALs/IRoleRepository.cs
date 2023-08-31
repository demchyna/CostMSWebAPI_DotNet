using CostMSWebAPI.Models;

namespace CostMSWebAPI.DALs;

public interface IRoleRepository
{
    Role? GetById(long? id);
    Role? GetByName(string? name);
    Role Create(Role role, out int affectedRows);
    void Update(Role role, out int affectedRows);
    void Delete(Role role, out int affectedRows);
    IEnumerable<Role> GetAll();
}
