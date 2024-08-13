using flashlightapi.Models;

namespace flashlightapi.Interfaces;

public interface IUserRepository
{
    public Task<List<User>> ListAsync();

    public Task<User?> GetByIdAsync(Guid id);

    public Task<User> CreateAsync(User userModel);

    public Task<User?> UpdateAsync(Guid id, User userModel);

    public Task<User?> DeleteAsync(Guid userId);
}