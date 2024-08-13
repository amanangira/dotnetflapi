using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flashlightapi.Models;

public class Base
{
    // TODO - convert to type UUID
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}