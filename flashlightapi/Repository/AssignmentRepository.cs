using flashlightapi.Data;
using flashlightapi.DTOs.common;
using flashlightapi.Models;
using flashlightapi.Repository;
using Microsoft.EntityFrameworkCore;
using Queryable = flashlightapi.DTOs.common.Queryable;

namespace flashlightapi.Interfaces;

public class AssignmentRepository(ApplicationDBContext _dbContext) : IAssignmentRepository
{
    private ApplicationDBContext _dbContext = _dbContext;
    
    public async Task<List<Assignment>> ListAsync(Queryable query)
    {
        var builder = _dbContext.Assignment.Include(s => s.CreatedBy).AsQueryable();
        if (query.Page > 0)
        {
            var skipCount = (int)(query.Page * (query.PageSize - 1));
            builder = builder.
                Skip(skipCount).
                Take((int)query.PageSize);
        }
        
        return await builder.ToListAsync();
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