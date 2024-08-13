using System.ComponentModel.DataAnnotations.Schema;

namespace flashlightapi.Models;


public enum ProviderType : byte
{
    Flashlight,
    Clever
};

public class User : Base
{
    public string Name { get; set; } = String.Empty;

    public string Email { get; set; } = String.Empty;

    // TODO - Convert to type enum for the DB type, to store strings instead of int in DB
    public ProviderType Source { get; set; } = ProviderType.Flashlight;
    
    public List<Assignment> Assignments { get; set; }
}