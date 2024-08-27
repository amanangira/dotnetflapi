using flashlightapi.DTOs.assignment;
using flashlightapi.Models;

namespace flashlightapi.Mappers;

public static class AssignmentMapper
{

    public static AssignmentDTO ToAssignmentDTO(this Assignment assignmentModel)
    {
        return new AssignmentDTO()
        {
            Id = assignmentModel.Id,
            Title = assignmentModel.Title,
            StartAt = assignmentModel.CreatedAt,
            CloseAt = assignmentModel.CloseAt,
            // CreatedById = assignmentModel.CreatedById,
        };
    }

    public static Assignment ToAssignmentModel(this AssignmentDTO assignmentDto)
    {
        return new Assignment()
        {
            Title = assignmentDto.Title,
            CloseAt = assignmentDto.CloseAt,
            StartAt = assignmentDto.StartAt,
            // CreatedById = assignmentDto.CreatedById,
        };
    }

    public static Assignment ToAssignmentModel(this CreateAssignmentDTO createAssignmentDto)
    {
        return new Assignment()
        {
            Title = createAssignmentDto.Title,
            CloseAt = createAssignmentDto.CloseAt,
            StartAt = createAssignmentDto.StartAt,
        };
    }
}