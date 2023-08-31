using CostMSWebAPI.Data;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.DALs.Impls;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Role? GetById(long? id)
    {
        return _dbContext.Roles.Find(id);
    }

    public Role? GetByName(string? name)
    {
        return _dbContext.Roles
            .FirstOrDefault(role => role.Name == name);
    }

    public Role Create(Role role, out int affectedRows)
    {
        _dbContext.Roles.Add(role);
        affectedRows = _dbContext.SaveChanges();
        return role;
    }

    public void Update(Role role, out int affectedRows)
    {
        _dbContext.Roles.Update(role);
        affectedRows = _dbContext.SaveChanges();
    }

    public void Delete(Role role, out int affectedRows)
    {

        _dbContext.Roles.Remove(role);
        affectedRows = _dbContext.SaveChanges();
    }

    public IEnumerable<Role> GetAll()
    {
        return _dbContext.Roles.ToList();
    }
}
