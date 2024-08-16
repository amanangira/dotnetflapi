using System.ComponentModel.DataAnnotations;
using flashlightapi.Models;

namespace flashlightapi.DTOs.assignment;

public class AssignmentCreateDTO
{
    [Required]
    public string Title { get; set; }

    [Required]
    public Guid CreatedById { get; set; }

    [Required]
    public DateTime StartAt { get; set; }

    [Required]
    public DateTime CloseAt { get; set; }
}