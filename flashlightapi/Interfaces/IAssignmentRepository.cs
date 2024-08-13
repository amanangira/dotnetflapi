using flashlightapi.Models;

namespace flashlightapi.Repository;

public interface IAssignmentRepository
{
    public Task<List<Assignment>> ListAsync();

    public Task<Assignment?> GetByIdAsync(Guid id);

    public Task<Assignment> CreateAsync(Assignment userModel);
}