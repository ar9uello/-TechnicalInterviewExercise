using Domain.Enums;

namespace Application.ViewModels;

public class GetTaskVm
{
    public required int TaskId { get; set; }
    public required string TaskName { get; set; }
    public required string TaskDescription { get; set; }
    public required TaskEntityStatus TaskStatus { get; set; }
}
