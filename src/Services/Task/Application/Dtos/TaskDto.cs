namespace Application.Dtos;

public class TaskDto
{
    public required Guid TaskID {  get; set; }
    public required string TaskName { get; set; }
    public required string TaskDescription { get; set; }
    public required string TaskStatus { get; set; }
}
