using System.ComponentModel.DataAnnotations;
using flashlightapi.Models;

namespace flashlightapi.DTOs;

public class CreateUserDTO
{
    [Required]
    public string Name { get; set; } = String.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = String.Empty;

    // TODO - Convert to type enum for the DB type, to store strings instead of int in DB
    public ProviderType Source { get; set; } = ProviderType.Flashlight;
}