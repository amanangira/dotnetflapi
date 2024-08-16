using flashlightapi.Models;

namespace flashlightapi.DTOs.assignment;

public class AssignmentDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    
    public Guid CreatedById { get; set; }
    
    public DateTime StartAt { get; set; }
    
    public DateTime CloseAt { get; set; }
    
    public AccountDTO CreatedBy { get; set; }
}