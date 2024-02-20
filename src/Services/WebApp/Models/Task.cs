
namespace WebApp.Models;

public class TaskEntity
{
    public int TaskId { get; set; }
    public required string TaskName { get; set; }
    public required string TaskDescription { get; set; }
    public required TaskEntityStatus TaskStatus { get; set; }
    public IEnumerable<TaskEntity>? Data { get; internal set; }
}

public enum TaskEntityStatus
{
    ToDo,
    InProgress,
    Blocked,
    Completed,
    Canceled
}
