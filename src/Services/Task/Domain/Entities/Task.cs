namespace Domain.Entities;

public class Task
{
    public required Guid TaskID { get; set; }
    public required string TaskName { get; set; }
    public required string TaskDescription { get; set; }
    public required TaskStatus TaskStatus { get; set; }
}
