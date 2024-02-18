using Domain.Enums;

namespace Domain.Entities;

public class TaskEntity
{
    public required int TaskId { get; set; }
    public required string TaskName { get; set; }
    public required string TaskDescription { get; set; }
    public required TaskEntityStatus TaskStatus { get; set; }
}
