using flashlightapi.Data;
using flashlightapi.Models;
using flashlightapi.Repository;
using Microsoft.EntityFrameworkCore;

namespace flashlightapi.Interfaces;

public class AssignmentRepository(ApplicationDBContext _dbContext) : IAssignmentRepository
{
    private ApplicationDBContext _dbContext = _dbContext;
    
    public async Task<List<Assignment>> ListAsync()
    {
        return await _dbContext.Assignment.ToListAsync();
    }

    public async Task<Assignment?> GetByIdAsync(Guid id)
    {
        return await _dbContext.FindAsync<Models.Assignment>(id);
    }

    public async Task<Assignment> CreateAsync(Assignment assignmentModel)
    {
        await _dbContext.AddAsync<Models.Assignment>(assignmentModel);
        await _dbContext.SaveChangesAsync();

        return assignmentModel;
    }
}