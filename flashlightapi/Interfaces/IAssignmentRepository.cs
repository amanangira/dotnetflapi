using flashlightapi.Models;
using Queryable = flashlightapi.DTOs.common.Queryable;

namespace flashlightapi.Repository;

public interface IAssignmentRepository
{
    public Task<List<Assignment>> ListAsync(Queryable query);

    public Task<Assignment?> GetByIdAsync(Guid id);

    public Task<Assignment> CreateAsync(Assignment userModel);
}