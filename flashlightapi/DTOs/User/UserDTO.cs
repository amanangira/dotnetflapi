using flashlightapi.DTOs.assignment;
using flashlightapi.Models;

namespace flashlightapi.DTOs;

public class UserDTO
{
    
    public Guid Id { get; set; } = Guid.Empty;
    
    public string Name { get; set; } = String.Empty;

    public string Email { get; set; } = String.Empty;

    // TODO - Convert to type enum for the DB type, to store strings instead of int in DB
    public ProviderType Source { get; set; } = ProviderType.Flashlight;
    
    public List<AssignmentDTO> Assignments { get; set; }
}