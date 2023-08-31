using CostMSWebAPI.Data;
using CostMSWebAPI.Models;

namespace CostMSWebAPI.DALs.Impls;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User? GetById(long? id)
    {
        return _dbContext.Users.Find(id);
    }

    public User? GetByUsername(string? username)
    {
        return _dbContext.Users
            .FirstOrDefault(user => user.Username == username);
    }

    public User Create(User user, out int affectedRows)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 10);
        _dbContext.Users.Add(user);
        affectedRows = _dbContext.SaveChanges();
        return user;
    }

    public void Update(User user, out int affectedRows)
    {
        _dbContext.Users.Update(user);
        affectedRows = _dbContext.SaveChanges();
    }

    public void Delete(User user, out int affectedRows)
    {

        _dbContext.Users.Remove(user);
        affectedRows = _dbContext.SaveChanges();
    }

    public IEnumerable<User> GetAll()
    {
        return _dbContext.Users.ToList();
    }
}
