using System.ComponentModel.DataAnnotations.Schema;

namespace flashlightapi.Models;

public class Assignment : Base
{
    public string Title { get; set; }
    
    public DateTime StartAt { get; set; }
    
    public DateTime CloseAt { get; set; }
    
    [Column("CreatedById")]
    [ForeignKey(nameof(AppUser))]
    public string CreatedById { get; set; }
    
    public virtual AppUser CreatedBy { get; set; }
}