using flashlightapi.Data;
using flashlightapi.Interfaces;
using flashlightapi.Models;
using Microsoft.EntityFrameworkCore;

namespace flashlightapi.Repository;

public class UserRepository(ApplicationDBContext dbContext) : IUserRepository
{
    private ApplicationDBContext _dbContext = dbContext;

    public async Task<List<User>> ListAsync()
    {
        return await _dbContext.User.Include(u => u.Assignments).ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.User.Include(u => u.Assignments).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<User> CreateAsync(User userModel)
    {
        await _dbContext.AddAsync<Models.User>(userModel);
        await _dbContext.SaveChangesAsync();

        return userModel;
    }

    public async Task<User?> UpdateAsync(Guid id, User userModel)
    {
        var existingModel = await GetByIdAsync(id);
        if (existingModel == null)
        {
            return null;
        }

        existingModel.Email = userModel.Email;
        existingModel.Name = userModel.Name;

        await _dbContext.SaveChangesAsync();
        
        return userModel;
    }

    public async Task<User?> DeleteAsync(Guid id)
    {
        var userModel = await GetByIdAsync(id);
        
        if (userModel == null)
        {
            return null;
        }

        _dbContext.User.Remove(userModel);
        await _dbContext.SaveChangesAsync();

        return userModel;
    }
}