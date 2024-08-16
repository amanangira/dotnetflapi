namespace flashlightapi.Models;

public class Assignment : Base
{
    public string Title { get; set; }
    
    public Guid CreatedById { get; set; }
    
    public DateTime StartAt { get; set; }
    
    public DateTime CloseAt { get; set; }
}